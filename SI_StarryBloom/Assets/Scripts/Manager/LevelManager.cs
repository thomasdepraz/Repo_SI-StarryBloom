using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Starting Position")]
    public List<Transform> startingPositions = new List<Transform>();

    [Header("Instantiated Objects Parent")]
    public Transform weaponsParent;
    public Transform knightsParent;

    internal void ClearArena()
    {
        List<GameObject> objects = new List<GameObject>();
        foreach(Transform child in weaponsParent)
        {
            objects.Add(child.gameObject);
        }

        foreach (Transform child in knightsParent)
        {
            objects.Add(child.gameObject);
        }

        while (objects.Count > 0)//particles here
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
        }
    }
}
