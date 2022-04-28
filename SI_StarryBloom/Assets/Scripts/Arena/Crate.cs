using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public List<GameObject> spawnItems;
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            CrateExplode();
        }
    }

    public void CrateExplode()
    {
        GameObject go = Instantiate(spawnItems[Random.Range(0, spawnItems.Count)], transform.position, transform.rotation);
        if(go.tag == "Knight")
        {
            go.GetComponent<KnightObject>().SetAnimState(KnightObject.AnimState.PANIC);
        }
        Instantiate(particle,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
