using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightObject : MonoBehaviour
{
    public Knight knight;
    public ConfigurableJoint headJoint;

    private void OnCollisionEnter(Collision collision)
    {
        if(!knight.IsRoot())
        {
            Debug.Log(collision.gameObject.name);
            var tower = knight.tower;

            tower.EjectKnights(this);
        }
    }

}
