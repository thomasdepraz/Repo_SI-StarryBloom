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
        //GameObject enteringParent = enteringObject.transform.parent.gameObject;

        if (enteringObject.GetComponent<Rigidbody>() != null)
        {
            
            enteringObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

        Debug.Log(enteringObject);
        //Debug.Log(enteringParent);

        /*if (enteringObject.GetComponentInParent<Controller>() != null)
        {
            Debug.Log("Controller");
            enteringObject.GetComponentInParent<Controller>().rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }*/
    }
}
