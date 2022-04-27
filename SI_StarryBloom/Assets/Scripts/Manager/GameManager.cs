using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("References")]
    public PlayersManager playersManager;

    [Header("Data")]
    public float gameLength;

    private void Awake()
    {
        CreateSingleton(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Get PlayersManager

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Appear players
        playersManager.SpawnPlayers();

        //Appear UI

        //Start Timer

        //
    }

    public void EndGame()
    {
        //Get winner
        Player winner = null;
        var players = playersManager.players;
        for (int i = 0; i < players.Count; i++)
        {
            if (winner = null) winner = players[i];
            if (players[i].tower.knights.Count > winner.tower.knights.Count)
            {
                winner = players[i];
            }
        }

        //Destroy towers

        //Reload arena (clean, stop throwers...)

        //Appear restart UI
        players[0].input.SwitchCurrentActionMap("UIPlayer");
        for (int i = 1; i < players.Count; i++)
        {
            players[i].input.SwitchCurrentActionMap("Empty");
        }

    }



}
