using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    [Header("Data")]
    public GameObject knightPrefab;
    public int knightCount;
    public int knightHeight;
    public Vector3 startingPos;

    public KnightTower tower;

    public Action buildComplete;

    // Start is called before the first frame update
    void Start()
    {
        List<KnightObject> knights = new List<KnightObject>();
        for (int i = 0; i < knightCount; i++)
        {
            GameObject go = Instantiate(knightPrefab, new Vector3(startingPos.x, startingPos.y + knightHeight * i+1, startingPos.z), Quaternion.identity);
            go.name = $"Knight_{i}";
            KnightObject ko = go.GetComponent<KnightObject>();
            Knight knight = new Knight(go);
            ko.knight = knight;

            knights.Add(ko);
        }
        tower = new KnightTower(knights);

        buildComplete?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
