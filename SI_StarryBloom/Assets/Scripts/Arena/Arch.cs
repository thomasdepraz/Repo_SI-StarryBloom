using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch : MonoBehaviour
{

    public List<Vector3> archPositions;
    public float timer;
    private bool archSpawned;
    public GameObject archObject;

    public float rangeY1; //limites pour la hauteur des arches
    public float rangeY2;

    private Vector3 firstArchPos;
    private Vector3 secondArchPos;

    private GameObject arch1;
    private GameObject arch2;

    private float newY1;
    private float newY2;

    private Vector3 finalPos1;
    private Vector3 finalPos2;

    void Start()
    {
        archSpawned = false;
        newY1 = Random.Range(rangeY1, rangeY2);
        newY2 = Random.Range(rangeY1, rangeY2);

        
    }

    
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && archSpawned == false)
        {            
            int i = Random.Range(0, archPositions.Count - 1);
            firstArchPos = archPositions[i];
            archPositions.RemoveAt(i);

            int j = Random.Range(0, archPositions.Count - 1);
            secondArchPos = archPositions[j];

            //StartCoroutine(SpawnArch(firstArchPos, secondArchPos));

            arch1 = Instantiate(archObject, firstArchPos, Quaternion.identity);
            arch2 = Instantiate(archObject, secondArchPos, Quaternion.identity);

            finalPos1.y = newY1;
            finalPos2.y = newY2;

            archSpawned = true;
        }

        if(archSpawned == true)
        {
            arch1.GetComponent<Rigidbody>().useGravity = false;//provisoire
            arch2.GetComponent<Rigidbody>().useGravity = false;//provisoire

            //Vector3 newPos1 = Vector3.Lerp(pos1, finalPos1, 1);
            //Vector3 newPos2 = Vector3.Lerp(pos2, finalPos2, 1);

            arch1.transform.position = Vector3.MoveTowards(arch1.transform.position, finalPos1, 0.1f);
            arch2.transform.position = Vector3.MoveTowards(arch2.transform.position, finalPos2, 0.1f);

            if (arch1.transform.position != finalPos1)
            {
                arch1.GetComponent<SphereCollider>().enabled = false; // type de collider à modifier
            }
            else
            {
                arch1.GetComponent<SphereCollider>().enabled = true; // type de collider à modifier
            }

            if (arch1.transform.position != finalPos2)
            {
                arch2.GetComponent<SphereCollider>().enabled = false; // type de collider à modifier
            }
            else
            {
                arch2.GetComponent<SphereCollider>().enabled = true; // type de collider à modifier
            }
        }
    } 
    
    void SpawnArch(Vector3 pos1, Vector3 pos2, GameObject ar1, GameObject ar2)
    {           

        
        

        //yield return null;
    }
}
