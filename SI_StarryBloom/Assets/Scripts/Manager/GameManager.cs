using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public AudioSource musicSource;
    
    public bool useTimer;

    public int heightObjective;


    private void Awake()
    {
        CreateSingleton(false);
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Get PlayersManager
        playersManager = PlayersManager.Instance;

        musicSource.Play();

        if (playersManager.players.Count>0)
        {
            yield return new WaitForSeconds(2.7f);
            StartGame();
        }

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
        musicSource.Stop();

        SoundManager.Instance.PlaySound("SFX_Finish", false);

        StopCoroutine(timer.clockSpendTime);

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
        Arch.Instance.HideArks();

        //stop throwers
        spawner.Stop();

        //Hide inGameUI
        timer.timerUI.SetActive(false);
        playerUI.HideUI();

        TimerDraw.doOnce = false;
        TimerDraw.doOnceOther = false;

        //set player controller map
        players[0].input.SwitchCurrentActionMap("UIPlayer");
        for (int i = 1; i < players.Count; i++)
        {
            players[i].input.SwitchCurrentActionMap("Empty");
        }

        EventSystem.current.SetSelectedGameObject(victoryScreen.restartButton);
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
