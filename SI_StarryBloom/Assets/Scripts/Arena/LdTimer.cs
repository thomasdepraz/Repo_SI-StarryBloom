using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LdTimer : MonoBehaviour
{
    public float timer;

    void Start()
    {
        
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
