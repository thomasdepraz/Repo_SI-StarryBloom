using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightObject : MonoBehaviour
{
    public Knight knight;

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

                tower.EjectKnights(this);
            }
        }

    }
    

}
