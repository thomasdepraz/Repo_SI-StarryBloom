using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch : Singleton<Arch>
{
    public void Awake()
    {
        CreateSingleton(false);
    }

    public List<GameObject> archs = new List<GameObject>();

    public void HideArks()
    {
        foreach(var ark in archs)
        {
            ark.SetActive(false);
        }
    }

    public void AppearArks()
    {
        int r = Random.Range(0, archs.Count-1);
        AppearObject(archs[r]);

        int otherR = r;
        while (otherR == r)
        {
            otherR = Random.Range(0, archs.Count - 1);
        }
        AppearObject(archs[otherR]);
    }

    public void AppearObject(GameObject obj)
    {
        obj.transform.position += Vector3.down * 8;
        obj.SetActive(true);

        LeanTween.moveY(obj, 0, 2).setOnComplete(()=> Debug.Log(""));
    }
}
