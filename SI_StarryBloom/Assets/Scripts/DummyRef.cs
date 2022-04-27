using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRef : MonoBehaviour
{
    public static DummyRef dR;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        dR = this;
    }
}
