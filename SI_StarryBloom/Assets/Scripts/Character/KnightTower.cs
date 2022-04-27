﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnightTower
{
    public Player player;

    public List<KnightObject> knights = new List<KnightObject>();
    public Knight root;

    public KnightTower(List<KnightObject> knights, Player player)
    {
        this.knights = knights;
        this.player = player;
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
        }

        root = newRoot;
        root.SetWeight(1000);
        root.rigidbody.constraints = UnityEngine.RigidbodyConstraints.FreezeRotation;
    }

    public void EjectKnights(KnightObject startKnight, Vector3 ejectDirection)
    {
        int index = knights.IndexOf(startKnight);

        int numberToEject = knights.Count - knights.IndexOf(startKnight);

        knights[knights.Count - 1].knight.DeleteJoint();

        for (int i = 0; i < numberToEject; i++)
        {
            knights[knights.Count - 1].knight.possessionState = Knight.PossessionState.NEUTRAL;

            knights[knights.Count - 2].knight.DeleteJoint();

            knights[knights.Count - 1].knight.rigidbody.AddForce((ejectDirection + (Vector3.up * Random.Range(-0.5f,0.5f)) + (Vector3.right * Random.Range(-0.5f, 0.5f))) * 2f, ForceMode.Impulse);

            knights[knights.Count - 1].knight.tower = null;

            knights.RemoveAt(knights.Count - 1);

            //TEMP
            //knights[i].gameObject.SetActive(false);
        }
        //var weapon = myPlayer.creator.WeaponCreation();
        //AttachWeapon(weapon);
    }

    public void AttachWeapon(GameObject weapon)
    {
        var topKnight = knights[knights.Count - 1].knight;
        weapon.transform.position = topKnight.transform.position + topKnight.transform.up * 1; //FIX MAGIC NUMBER
        topKnight.SetJoint(weapon);
    }

    public void ThrowWeapon(Vector3 direction)
    {
        var topKnight = knights[knights.Count - 1].knight;
        var weaponRb = topKnight.joint.connectedBody;
        if(weaponRb!=null)
        {
            GameObject.Destroy(topKnight.joint);
            weaponRb.velocity = Vector3.zero;
            weaponRb.AddForce(direction * 25, ForceMode.Impulse);
        }
    }

    public void AddKnight(KnightObject newKnight, Player player)
    {
        var rootPosition = knights[0].transform.position;

        //Add to list and set root
        knights.Insert(0, newKnight);
        SetRoot(newKnight.knight);

        //Set controller
        newKnight.transform.position = rootPosition + Vector3.down * 1;//FIX MAGIC NUMBER
        newKnight.transform.forward = knights[1].knight.transform.forward;
        player.controller.rb = newKnight.knight.rigidbody;

        //Set joint
        newKnight.knight.SetJoint(knights[1].knight);

    }

}
