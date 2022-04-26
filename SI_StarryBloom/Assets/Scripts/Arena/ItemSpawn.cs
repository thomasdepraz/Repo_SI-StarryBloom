using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> spawnItems;

    public GameObject spawnTarget;

    private bool spawnCooldownOn = false;

    public void Start()
    {
        Spawn();
    }

    public void Update()
    {
        if (Input.GetKeyDown ("down"))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(spawnItems[Random.Range(0, spawnItems.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.rotation);
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        Debug.Log("YEET!");
        Spawn();
    }

}
