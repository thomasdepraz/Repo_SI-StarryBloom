using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerLobbySpawn : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public GameObject startGameButton;
    public GameObject infoMessage;

    public List<GameObject> players = new List<GameObject>();

    [Header("Panels")]
    public List<GameObject> joinedPlayers;

    public void Start()
    {
        playerInputManager.onPlayerJoined += Join;
        playerInputManager.onPlayerLeft += Disconnect;
    }

    public void Join(PlayerInput player)
    {
        players.Add(player.transform.gameObject);

        //Set active the correct model
        joinedPlayers[players.Count-1].SetActive(true);

        if(players.Count >= 1)
        {
            infoMessage.SetActive(false);
        }

        if(players.Count > 1)
        {
            startGameButton.SetActive(true);
        }
    }

    public void Disconnect(PlayerInput player)
    {
        players.Remove(player.transform.gameObject);

        joinedPlayers[players.Count - 1].SetActive(false);

        if (players.Count == 0)
        {
            infoMessage.SetActive(true);
        }

        if (players.Count <= 1)
        {
            startGameButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }


}