using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIDraw : MonoBehaviour
{
    public List<GameObject> playersInGame;
    public List<TextMeshProUGUI> counters = new List<TextMeshProUGUI>();

    public void AppearUI(int numOfPlayers)
    {
        for (int i = 0; i < numOfPlayers; i++)
        {
            playersInGame[i].SetActive(true);
        }
    }

    public void HideUI()
    {
        for (int i = 0; i < playersInGame.Count; i++)
        {
            playersInGame[i].SetActive(false);
            LeanTween.cancel(playersInGame[i]);
        }
    }

    public void UpdateCounter(int number, int player)
    {
        counters[player].text = $"{number}/10";

        //Set tweening
        if(number == 7)
        {
            LeanTween.scale(playersInGame[player], Vector3.one * 1.2f, 0.2f).setLoopPingPong(1000000);
        }
        else if(number < 7)
        {
            LeanTween.cancel(playersInGame[player]);
        }
    }
}
