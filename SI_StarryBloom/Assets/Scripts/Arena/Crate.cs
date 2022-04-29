using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    List<List<GameObject>> spawnItems = new List<List<GameObject>>();
    public List<GameObject> knightItems;
    public List<GameObject> weaponItems;
    public GameObject particle;

    public GameObject dummyPrefab;

    private void Start()
    {
        spawnItems.Add(knightItems);
        spawnItems.Add(weaponItems);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            CrateExplode();
        }
    }

    public void CrateExplode()
    {
        int weaponN = GameManager.Instance.levelManager.weaponsParent.childCount;
        int knightN = GameManager.Instance.levelManager.knightsParent.childCount;

        int weaponBonus = 0;
        int knightBonus = 0;

        int r = 0;

        GameObject go;

        if (weaponN >= knightN)
        {
            knightBonus = weaponN - knightN;

            r = Random.Range(0, knightBonus + 1);

            if (r == 0)
            {
                go = Instantiate(weaponItems[Random.Range(0, weaponItems.Count)], transform.position, transform.rotation);
            }
            else
            {
                go = Instantiate(knightItems[Random.Range(0, knightItems.Count)], transform.position, transform.rotation);
            }

        }
        else
        {
            weaponBonus = knightN - weaponN;

            r = Random.Range(0, weaponBonus + 1);

            if (r == 0)
            {
                go = Instantiate(knightItems[Random.Range(0, knightItems.Count)], transform.position, transform.rotation);
            }
            else
            {
                go = Instantiate(weaponItems[Random.Range(0, weaponItems.Count)], transform.position, transform.rotation);
            }
        }

        if(go.tag == "Knight")
        {
            KnightObject ko = go.GetComponent<KnightObject>();

            ko.SetAnimState(KnightObject.AnimState.PANIC);

            Knight knight = new Knight(go);
            ko.knight = knight;

            ko.knight.dummyPrefab = dummyPrefab;

            go.transform.SetParent(GameManager.Instance.levelManager.knightsParent);
        }
        else
        {
            go.transform.SetParent(GameManager.Instance.levelManager.weaponsParent);
        }
        
        Instantiate(particle,transform.position, Quaternion.identity);
        Destroy(gameObject);

        //Sound
        SoundManager.Instance.PlaySound(go.tag == "Knight" ? "SFX_Throw2" : "SFX_Throw1", false);
    }
}
