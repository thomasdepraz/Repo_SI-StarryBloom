using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
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

                    Debug.Log("CanBePickedUp");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Knight")
        {
            KnightObject ko = other.transform.GetComponent<KnightObject>();
            Player p = ko.transform.parent.GetComponent<Player>();
            p.controller.pickupInRange = null;
        }
    }
}
