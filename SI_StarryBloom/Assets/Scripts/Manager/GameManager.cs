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
    public ItemSpawn spawner;
    public VictoryScript victoryScreen;
    
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

        //Start spawner
        spawner.StartSpawner();
    }

    public void EndGame()
    {
        //Get order
        var players = playersManager.players;

        players.Sort();


        //Destroy towers
        foreach(var p in players)
        {
            p.DestroyTower();
        }


        //Reload arena (clean, stop throwers...)
        levelManager.ClearArena();

        //stop throwers
        spawner.Stop();

        //Hide inGameUI
        timer.timerUI.SetActive(false);
        playerUI.HideUI();

        //set player controller map
        players[0].input.SwitchCurrentActionMap("UIPlayer");
        for (int i = 1; i < players.Count; i++)
        {
            players[i].input.SwitchCurrentActionMap("Empty");
        }

        //Appear restart UI
        victoryScreen.SetupScreen(players);
    }

    public void UpdatePlayer(Player p)
    {
        var number = playersManager.players.IndexOf(p);
        var height = p.tower.knights.Count;
        playerUI.UpdateCounter(height,number);
    }



}
