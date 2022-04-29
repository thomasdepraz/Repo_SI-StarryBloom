using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public List<GameObject> spawnItems;
    public GameObject particle;

    public GameObject dummyPrefab;

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
            KnightObject ko = go.GetComponent<KnightObject>();

            ko.SetAnimState(KnightObject.AnimState.PANIC);

            Knight knight = new Knight(go);
            ko.knight = knight;

            ko.knight.dummyPrefab = dummyPrefab;
        }
        Instantiate(particle,transform.position, Quaternion.identity);
        Destroy(gameObject);

        //Sound
        SoundManager.Instance.PlaySound(go.tag == "Knight" ? "SFX_Throw2" : "SFX_Throw1", false);
    }
}
