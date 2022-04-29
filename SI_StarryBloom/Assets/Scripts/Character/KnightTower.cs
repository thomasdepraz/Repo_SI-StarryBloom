using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnightTower
{
    public Player player;

    public List<KnightObject> knights = new List<KnightObject>();
    public Knight root;
    public WeaponController currentWeapon;

    public KnightTower(List<KnightObject> knights, Player player)
    {
        this.knights = knights;
        this.player = player;
        player.tower = this;
        InitializeKnights();
    }

    private void InitializeKnights()
    {
        for (int i = 0; i < knights.Count; i++)
        {
            knights[i].knight.SetPlayer(player);


            if(i<knights.Count-1)
            {
                knights[i].knight.SetJoint(knights[i + 1].knight);
            }
        }

        //set base knight weight
        SetRoot(knights[0].knight);
    }

    private void SetRoot(Knight newRoot)
    {
        if (root != null)
        {
            root.SetWeight(1);
            root.rigidbody.constraints = UnityEngine.RigidbodyConstraints.None;
            GameManager.Instance.cameraManager.RemoveTransformFromGroup(root.transform);
        }

        root = newRoot;
        root.SetWeight(1000);
        root.rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;

        //set camera target group
        GameManager.Instance.cameraManager.AddTransformToGroup(root.transform);
    }

    public void EjectKnights(KnightObject startKnight, Vector3 ejectDirection)
    {
        int index = knights.IndexOf(startKnight);

        int numberToEject = knights.Count - knights.IndexOf(startKnight);

        //knights[knights.Count - 1].knight.joint.connectedBody.AddForce((ejectDirection + (Vector3.up * Random.Range(-1f, 1f)) + (Vector3.right * Random.Range(-1f, 1f))) * 2f, ForceMode.Impulse);

        ChangeWeaponTag("PickUpWeapon");

        DetachWeapon();

        for (int i = 0; i < numberToEject; i++)
        {
            knights[knights.Count - 2].knight.DeleteJoint();

            knights[knights.Count - 1].knight.rigidbody.AddForce((ejectDirection + (Vector3.up * Random.Range(-1f,1f)) + (Vector3.right * Random.Range(-1f, 1f))) * 2f, ForceMode.Impulse);


            if(knights[knights.Count - 1].knight.healthState == Knight.HealthState.ARMORED)
            {
                knights[knights.Count - 1].SetHealthState(Knight.HealthState.NAKED);
            }
            else
            {
                knights[knights.Count - 1].StartCoroutine( knights[knights.Count - 1].Destruction());
            }

            knights[knights.Count - 1].knight.SetPlayer(null);

            knights[knights.Count - 1].SetAnimState(KnightObject.AnimState.PANIC);

            knights.RemoveAt(knights.Count - 1);
        }

        //Sound
        SoundManager.Instance.PlaySound("SFX_CounterSword3", false);

        //check startknight health state
        //if(startKnight.knight.healthState == Knight.HealthState.ARMORED)
        //{
        //    startKnight.SetHealthState(Knight.HealthState.NAKED);
        //}
        //else
        //{
        //    //poof particle
        //    GameObject.Instantiate(startKnight.poofParticle, startKnight.transform.position, Quaternion.identity);

        //    //destroy object
        //    GameObject.Destroy(startKnight);
        //}


        GameManager.Instance.UpdatePlayer(player);
    }

    public void DetachWeapon()
    {
        knights[knights.Count - 1].knight.DeleteJoint();

        //ChangeWeaponTag("PickUpWeapon");

        currentWeapon = null;
    }

    public void AttachWeapon(WeaponController weapon)
    {
        var topKnight = knights[knights.Count - 1].knight;
        Debug.Log(weapon);
        Debug.Log(topKnight);
        weapon.transform.position = topKnight.transform.position + topKnight.transform.up * 1; //FIX MAGIC NUMBER
        weapon.transform.rotation = topKnight.transform.rotation;
        currentWeapon = weapon;
        ChangeWeaponTag("Weapon");
        topKnight.SetJoint(weapon.gameObject);

        //Sound
        SoundManager.Instance.PlaySound("SFX_NewSword", false);
    }

    public void ThrowWeapon(Vector3 direction)
    {
        var topKnight = knights[knights.Count - 1].knight;
        var weaponRb = topKnight.joint.connectedBody;
        var cWeapon = currentWeapon;
        if(weaponRb!=null)
        {
            player.StartCoroutine(player.RotateRigibody(weaponRb, currentWeapon));

            DetachWeapon();

            weaponRb.velocity = Vector3.zero;
            weaponRb.AddForce(cWeapon.transform.forward * 2f, ForceMode.Impulse);

            //Sound
            SoundManager.Instance.PlaySound("SFX_Throw3", false);
        }
    }

    public void AddKnight(KnightObject newKnight, Player player)
    {
        var rootPosition = knights[0].transform.position;

        GameObject.Instantiate(newKnight.poofParticle,rootPosition, Quaternion.identity);

        //Anims
        knights[0].SetAnimState(KnightObject.AnimState.DEFAULT);
        newKnight.SetAnimState(KnightObject.AnimState.DEFAULT);

        //Add to list and set root
        knights.Insert(0, newKnight);
        SetRoot(newKnight.knight);

        //Set controller
        newKnight.transform.position = rootPosition + Vector3.down * 1;//FIX MAGIC NUMBER
        newKnight.transform.forward = knights[1].knight.transform.forward;
        player.controller.rb = newKnight.knight.rigidbody;

        //Set joint
        newKnight.knight.SetJoint(knights[1].knight);

        GameManager.Instance.UpdatePlayer(player);

        //Sound
        SoundManager.Instance.PlaySound("SFX_NewKnight", false);
    }

    public void ChangeWeaponTag(string newTag)
    {
        if (currentWeapon == null)
            return;

        currentWeapon.transform.tag = newTag;

        int childNumber = currentWeapon.transform.childCount;
        for(int i = 0; i< childNumber; i++)
        {
            currentWeapon.transform.GetChild(i).tag = newTag;
        }
    }

}
