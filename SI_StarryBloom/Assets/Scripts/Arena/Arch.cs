using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch : MonoBehaviour
{

    public List<GameObject> archs;
    public float timer;
    private bool archSpawned;


    void Start()
    {
        archSpawned = false;     
    }

    
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && archSpawned == false)
        {            
            int i = Random.Range(0, archs.Count - 1);
            archs[i].SetActive(true);
            archs.RemoveAt(i);

            int j = Random.Range(0, archs.Count - 1);
            archs[j].SetActive(true);            

            archSpawned = true;
        }
    }     
}
