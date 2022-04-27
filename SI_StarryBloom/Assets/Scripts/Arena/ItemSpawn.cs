using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject spawnedCrate;

    public GameObject spawnTarget;

    public float minThrowStrength;
    public float maxThrowStrength;

    public void Start()
    {
        Spawn(); //why tf does it not wait for the coroutine tho?
    }

    private void Spawn()
    {
        StartCoroutine(SpawnTimer());
        var clone = Instantiate(spawnedCrate, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce((spawnTarget.transform.position - clone.transform.position) * Random.Range(minThrowStrength, maxThrowStrength));
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        Spawn();
    }

}
