using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("References")]
    [HideInInspector] public PlayersManager playersManager;
    public LevelManager levelManager;
    public GameTimer timer;
    public GameUIDraw playerUI;
    public CameraManager cameraManager;
    
    public bool useTimer;


    private void Awake()
    {
        CreateSingleton(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Get PlayersManager
        playersManager = PlayersManager.Instance;

        //StartGame();

    }

    [ContextMenu("StartGame")]
    public void StartGame()
    {
        //Appear players
        playersManager.SpawnPlayers();

        //Appear UI
        timer.timerUI.SetActive(true);

        playerUI.AppearUI(playersManager.players.Count);

        //Update
        foreach(var p in playersManager.players)
        {
            UpdatePlayer(p);
        }

        //Start Timer
        if(useTimer)
        {
            timer.RebootTimer();
        }

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
        foreach(var p in players)
        {
            p.DestroyTower();
        }


        //Reload arena (clean, stop throwers...)
        levelManager.ClearArena();

        //stop throwers

        //Appear restart UI


        //set player controller map
        players[0].input.SwitchCurrentActionMap("UIPlayer");
        for (int i = 1; i < players.Count; i++)
        {
            players[i].input.SwitchCurrentActionMap("Empty");
        }



    }

    public void UpdatePlayer(Player p)
    {
        var number = playersManager.players.IndexOf(p);
        var height = p.tower.knights.Count;
        playerUI.UpdateCounter(height,number);
    }



}
