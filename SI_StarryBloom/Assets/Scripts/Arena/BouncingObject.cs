using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObject : MonoBehaviour
{

    public float bounceForce;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject enteringObject = other.gameObject;
        if(enteringObject.tag == "Knight")
        {
            Player p = enteringObject.GetComponent<KnightObject>().knight.tower.player;
            Controller controller = p.controller;

            Rigidbody rb = controller.rb;

            if (rb != null)
            {
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }

        }
        
        //Debug.Log(enteringParent);

        /*if (enteringObject.GetComponentInParent<Controller>() != null)
        {
            Debug.Log("Controller");
            enteringObject.GetComponentInParent<Controller>().rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }*/
    }
}
