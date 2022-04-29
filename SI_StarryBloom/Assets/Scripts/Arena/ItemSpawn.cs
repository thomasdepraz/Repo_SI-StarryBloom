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
    private Coroutine spawnRoutine;
    public void StartSpawner()
    {
        spawnRoutine = StartCoroutine(SpawnTimer());
    }

    public void Stop()
    {
        StopCoroutine(spawnRoutine);
    }

    private void Spawn()
    {
        var target = spawnTarget.transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f,5f));
        var clone = Instantiate(spawnedCrate, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce( (target - clone.transform.position) * Random.Range(minThrowStrength, maxThrowStrength) );
        spawnRoutine = StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        Spawn();
    }

}
