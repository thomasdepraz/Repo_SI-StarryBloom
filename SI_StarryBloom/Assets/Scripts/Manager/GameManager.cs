using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("References")]
    public PlayersManager playersManager;
    public GameTimer timer;

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
        playersManager = PlayersManager.Instance;

        StartGame();

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
        timer.timerUI.SetActive(true);

        //Start Timer
        timer.RebootTimer();

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

        //Check draw
        foreach(var p in players)
        {
            if (p != winner && p.tower.knights.Count == winner.tower.knights.Count)
                winner = null;
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
