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
        }
    }

    public void UpdateCounter(int number, int player)
    {
        counters[player].text = number.ToString();
    }
}
