using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject dummyPrefab;
    public GameObject defaultWeaponPrefab;
    public int knightCount;
    public int knightHeight;
    public Vector3 startingPos;

    public KnightTower tower;

    public Action buildComplete;

    public void BuildTower(Vector3 originPosition, Player player)
    {
        List<KnightObject> knights = new List<KnightObject>();
        for (int i = 0; i < knightCount; i++)
        {
            GameObject go = Instantiate(knightPrefab, new Vector3(originPosition.x, originPosition.y + knightHeight * i + 1, originPosition.z), Quaternion.identity, transform);
            go.name = $"Knight_{i}";
            KnightObject ko = go.GetComponent<KnightObject>();
            Knight knight = new Knight(go);
            ko.knight = knight;
            ko.knight.dummyPrefab = dummyPrefab;

            knights.Add(ko);
        }
        tower = new KnightTower(knights, player);

        var weapon = WeaponCreation();
        tower.AttachWeapon(weapon);

        buildComplete?.Invoke();
    }

    public WeaponController WeaponCreation()
    {
        var topKnight = tower.knights[tower.knights.Count - 1].knight;
        return Instantiate(defaultWeaponPrefab, topKnight.transform.position + topKnight.transform.up * 1, Quaternion.identity).GetComponent<WeaponController>();
    }
}
