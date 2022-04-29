using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightObject : MonoBehaviour
{
    public Knight knight;
    public Collider col;
    public SkinnedMeshRenderer rend;
    public SkinnedMeshRenderer rend2;
    public Animator anim;
    public bool invincible = false;

    [Header("Particles")]
    public GameObject impactParticle;
    public GameObject poofParticle;
    public GameObject sweatParticle;

    public void Start()
    {
        if(knight.rigidbody == null)
            knight = new Knight(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (knight.possessionState == Knight.PossessionState.NEUTRAL && collision.gameObject.tag == "Floor")
        {
            knight.StartPanikIdle();
        }

        if (knight.possessionState == Knight.PossessionState.POSSESSED && collision.gameObject.tag == "Knight")
        {
            if(knight.IsRoot())
            {
                var player = transform.parent.gameObject.GetComponent<Player>();

                //if (!player.controller.isGrounded())
                {
                    KnightObject k = collision.gameObject.GetComponent<KnightObject>();

                    if (k.knight.tower == null)//the knight has no team
                    {
                        //Possess
                        player.tower.AddKnight(k, player);
                        k.knight.SetPlayer(player);

                        k.sweatParticle.SetActive(false);
                    }
                }
            }
        }
        else
        {

        }

        if(knight.possessionState == Knight.PossessionState.POSSESSED)
        {
            if (knight.tower.knights.Count > 0 && !knight.IsRoot() && collision.gameObject.tag == "Weapon" && invincible == false)
            {
                if (knight.tower.currentWeapon != collision.rigidbody.gameObject)
                {
                    Debug.Log(collision.gameObject.name);
                    var tower = knight.tower;

                    Rigidbody weaponRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                    /*ContactPoint[] contactPoints = collision.contacts;

                    Vector3 impulsePoint = contactPoints[0].point;

                    int w = 1;

                    for(int i = 0; i < contactPoints.Length; i++)
                    {
                        impulsePoint = ((impulsePoint * w)  + contactPoints[i].point)/(w+1);
                    }*/

                    //Impact particle
                    Instantiate(impactParticle, collision.contacts[0].point, Quaternion.identity);

                    //Sound
                    SoundManager.Instance.PlaySound("SFX_SwordAttack3", false);

                    Vector3 ejectForce = weaponRigidbody.velocity;

                    ejectForce = new Vector3(ejectForce.x, 0f, ejectForce.z);

                    tower.EjectKnights(this, ejectForce);

                    KnightObject rootKO = tower.root.transform.gameObject.GetComponent<KnightObject>();

                    rootKO.StartCoroutine(rootKO.InvincibilityFrame());

                    collision.gameObject.GetComponent<WeaponController>().DestroyWeapon();
                }
            }
        }

    }
    
    IEnumerator InvincibilityFrame()
    {

        for (int i = 0; i < knight.tower.knights.Count; i++)
        {
            knight.tower.knights[i].invincible = true;
        }

        yield return new WaitForSeconds(0.02f);

        for (int x = 0; x <4; x ++)
        {
            for (int i = 0; i < knight.tower.knights.Count; i++)
            {
                knight.tower.knights[i].rend.material.SetFloat("_HitColor", 0.2f);
            }

            yield return new WaitForSeconds(0.15f);

            for (int i = 0; i < knight.tower.knights.Count; i++)
            {
                knight.tower.knights[i].rend.material.SetFloat("_HitColor", 0f);
            }

            yield return new WaitForSeconds(0.15f);
        }

        for (int i = 0; i < knight.tower.knights.Count; i++)
        {
            knight.tower.knights[i].rend.material.SetFloat("_HitColor", 0.2f);
        }

        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < knight.tower.knights.Count; i++)
        {
            knight.tower.knights[i].rend.material.SetFloat("_HitColor", 0f);
        }

        for (int i = 0; i < knight.tower.knights.Count; i++)
        {
            knight.tower.knights[i].invincible = false;
        }
    }

    public enum AnimState {DEFAULT, WALKING, PANIC};
    public void SetAnimState(AnimState state)
    {
        switch (state)
        {
            case AnimState.DEFAULT:
                anim.SetBool("isMoving", false);
                anim.SetBool("isPanic", false);
                break;
            case AnimState.WALKING:
                anim.SetBool("isMoving", true);
                anim.SetBool("isPanic", false);
                break;
            case AnimState.PANIC:
                anim.SetBool("isMoving", false);
                anim.SetBool("isPanic", true);
                break;
            default:
                break;
        }
    }

    public void SetHealthState(Knight.HealthState state)
    {
        knight.healthState = state;
        switch (state)
        {
            case Knight.HealthState.ARMORED:

                //deactivate rend
                rend2.enabled = true;

                //activate particle
                sweatParticle.SetActive(false);
                break;
            case Knight.HealthState.NAKED:

                //deactivate rend
                rend2.enabled = false;

                //activate particle
                sweatParticle.SetActive(true);
                break;
            default:
                break;
        }
    }

    public IEnumerator Destruction()
    {
        yield return new WaitForSeconds(0.02f);

        rend.material.SetInteger("_Dead", 0);

        rend.material.SetFloat("_HitColor", 0.2f);

        //Sound
        SoundManager.Instance.PlaySound("SFX_DeathKnight", false);

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
