using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    List<Player> playersInRange = new List<Player>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knight")
        {
            KnightObject ko = other.transform.GetComponent<KnightObject>();

            if (ko.knight.possessionState == Knight.PossessionState.POSSESSED)
            {

                if (ko.knight.IsRoot() == true)
                {
                    Player p = ko.transform.parent.GetComponent<Player>();

                    p.controller.pickupInRange = this;
                    playersInRange.Add(p); ;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Knight")
        {
            KnightObject ko = other.transform.GetComponent<KnightObject>();

            if (ko.knight.possessionState == Knight.PossessionState.POSSESSED)
            {
                if (ko.knight.IsRoot() == true)
                {
                    Player p = ko.transform.parent.GetComponent<Player>();
                    p.controller.pickupInRange = null;
                    playersInRange.Remove(p);
                }
            }
        }
    }

    public void Grab()
    {
        foreach(var p in playersInRange)
        {
            p.controller.pickupInRange = null;
        }
        playersInRange.Clear();
    }
}
