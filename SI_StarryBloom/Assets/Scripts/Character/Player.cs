using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string ID;
    public CharacterCreator creator;
    public Controller controller;
    public KnightTower tower;

    // Start is called before the first frame update
    void Start()
    {
        creator.buildComplete += InitPlayer;
    }

   

    private void InitPlayer()
    {
        tower = creator.tower;
        controller.rb = GetRootRigidbody();
    }

    private Rigidbody GetRootRigidbody()
    {
        return tower.root.rigidbody;
    }
}
