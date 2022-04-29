using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IComparable
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
        input.SwitchCurrentActionMap("Player");
    }

    private Rigidbody GetRootRigidbody()
    {
        return tower.root.rigidbody;
    }

    public void DestroyTower()
    {
        controller.enabled = false;

        var knights = tower.knights;
        while(knights.Count > 0)
        {
            Instantiate(knights[knights.Count - 1].poofParticle, knights[knights.Count - 1].gameObject.transform.position, Quaternion.identity);
            GameObject.Destroy(knights[knights.Count-1].gameObject);
            knights.RemoveAt(knights.Count - 1);
        }

        tower = null;
    }

    public IEnumerator RotateRigibody(Rigidbody rb, WeaponController wc)
    {
        yield return new WaitForSeconds(0.05f);

        //rb.angularVelocity = new Vector3(0f, Mathf.Deg2Rad * 360f, 0f) * 10f;
        rb.AddTorque(rb.transform.up * 20f, ForceMode.VelocityChange);

        wc.StartThrowState();
    }

    public int CompareTo(object other)
    {
        if (other == null) return 1;

        Player otherPlayer = other as Player;

        return tower.knights.Count.CompareTo(otherPlayer.tower.knights.Count);
    }
}
