using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public string ID;
    public PlayerInput input;
    public CharacterCreator creator;
    public Controller controller;
    public KnightTower tower;

    // Start is called before the first frame update
    void Start()
    {
        creator.buildComplete += InitPlayer;
        controller.enabled = false;
    }

    public void AppearPlayer(Vector3 position)
    {
        //Create tower
        creator.BuildTower(position, this);
    }
   

    private void InitPlayer()
    {
        tower = creator.tower;
        controller.rb = GetRootRigidbody();
        controller.controlledTower = tower;
        controller.enabled = true;
    }

    private Rigidbody GetRootRigidbody()
    {
        return tower.root.rigidbody;
    }
}
