using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightObject : MonoBehaviour
{
    public Knight knight;
    public SkinnedMeshRenderer rend;

    public void Start()
    {
        if(knight.rigidbody == null)
            knight = new Knight(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(knight.possessionState == Knight.PossessionState.NEUTRAL && collision.gameObject.tag == "Player")
        {
            var player = collision.transform.parent.gameObject.GetComponent<Player>();

            if(player.controller.IsFalling())
            {
                if (knight.tower == null)//the knight has no team
                {
                    //Possess
                    player.tower.AddKnight(this, player);
                    knight.SetPlayer(player);
                }
                else
                {

                }
            }
        }
        else
        {

        }

        if(knight.possessionState == Knight.PossessionState.POSSESSED)
        {
            if (knight.tower.knights.Count > 0 && !knight.IsRoot() && collision.gameObject.tag == "Weapon" )
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

                Vector3 ejectForce = weaponRigidbody.velocity;

                ejectForce = new Vector3(ejectForce.x, 0f, ejectForce.z);

                tower.EjectKnights(this, ejectForce);
            }
        }

    }
    

}
