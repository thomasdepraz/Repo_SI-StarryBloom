using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Rigidbody rb;

    bool throwed = false;

    float baseAngularDrag = 0f;

    private void Start()
    {
        baseAngularDrag = rb.angularDrag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(throwed)
            StartCoroutine(EndThrowState());
    }

    public void StartThrowState()
    {
        throwed = true;

        transform.tag = "Weapon";

        rb.angularDrag = 0;

        rb.useGravity = false;
    }

    private IEnumerator EndThrowState()
    {
        yield return new WaitForSeconds(0.05f);

        throwed = false;

        transform.tag = "PickUpWeapon";

        rb.angularDrag = baseAngularDrag;

        rb.useGravity = true;
    }
}
