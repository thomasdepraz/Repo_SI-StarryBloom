using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public GameObject screen;
    public GameObject restartButton;

    public Sprite sunSprite;
    public Sprite moonSprite;
    public Sprite crossSprite;
    public Sprite cloverSprite;

    public List<Image> heads = new List<Image>();

    public void Start()
    {
        EventSystem.current.firstSelectedGameObject = restartButton;
    }

    public void SetupScreen(List<Player> players)
    {
        screen.SetActive(true);


        for (int i = 0; i < players.Count; i++)
        {
            heads[i].gameObject.SetActive(true);
            switch(players[i].ID)
            {
                case "Player_1":
                    heads[i].sprite = sunSprite;
                    break;
                case "Player_2":
                    heads[i].sprite = moonSprite;
                    break;
                case "Player_3":
                    heads[i].sprite = crossSprite;
                    break;
                case "Player_4":
                    heads[i].sprite = cloverSprite;
                    break;
            }
        }

    }

    public void RestartGame()
    {
        for (int i = 0; i < heads.Count; i++)
        {
            heads[i].gameObject.SetActive(true);
        }
        screen.SetActive(false);

        GameManager.Instance.StartGame();
    }
    public void MainMenu()
    {

    }
}
