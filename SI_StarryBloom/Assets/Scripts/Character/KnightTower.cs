﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KnightTower
{
    public Player myPlayer;

    public List<KnightObject> knights = new List<KnightObject>();
    public Knight root;

    public KnightTower(List<KnightObject> knights)
    {
        this.knights = knights;
        InitializeKnights();
    }

    private void InitializeKnights()
    {
        for (int i = 0; i < knights.Count; i++)
        {
            knights[i].knight.SetTower(this);
            if(i<knights.Count-1)
            {
                knights[i].knight.SetJoint(knights[i + 1].knight);
            }
        }

        //set base knight weight
        SetRoot(knights[0].knight);

        //GameObject.Destroy(knights[knights.Count-1].knight.joint);
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

    public void EjectKnights(KnightObject startKnight)
    {
        int index = knights.IndexOf(startKnight);

        for (int i = knights.Count - 1; i >= index; i--)
        {
            //TEMP
            knights[i].gameObject.SetActive(false);
        }

        var weapon = myPlayer.creator.WeaponCreation();
        AttachWeapon(weapon);
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

}
