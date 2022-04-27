using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public List<GameObject> spawnItems;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            CrateExplode();
        }
    }

    public void CrateExplode()
    {
        Instantiate(spawnItems[Random.Range(0, spawnItems.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
