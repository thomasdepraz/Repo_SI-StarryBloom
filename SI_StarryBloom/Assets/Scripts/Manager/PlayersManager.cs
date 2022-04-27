using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : Singleton<PlayersManager>
{
    public PlayerInputManager inputManager;
    public List<Player> players = new List<Player>();

    public List<Vector3> startingPos = new List<Vector3>();

    public PlayerSkins knightSkinsScheme;

    private void Awake()
    {
        CreateSingleton(true);
    }


    private void Start()
    {
        inputManager.onPlayerJoined += Join;
        inputManager.onPlayerLeft += Disconnect;
        inputManager.EnableJoining();
    }

    public void Join(PlayerInput playerInput)
    {
        playerInput.transform.SetParent(transform);
        var player = playerInput.gameObject.GetComponent<Player>();
        players.Add(player);

        //set player ID
        player.ID = $"Player_{players.Count}";

        if(SceneManager.GetActiveScene().name == "LobbyMenu")
        {
            //set playerInput mode
            if (players.Count == 1) player.input.SwitchCurrentActionMap("UIPlayer");
            else player.input.SwitchCurrentActionMap("Empty");
        }
        else//Debug scene
        {
            player.input.SwitchCurrentActionMap("Controller");
        }

    }
    public void Disconnect(PlayerInput playerInput)
    {
        var player = playerInput.gameObject.GetComponent<Player>();


        //Destroy player object
        players.Remove(player);
    }

    [ContextMenu("SpawnPlayers")]
    public void SpawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].AppearPlayer(startingPos[i]);
        }
    }

}

