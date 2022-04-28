using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIDraw : MonoBehaviour
{
    [Range(1,4)]
    public int numberOfPlayers;
    public List<GameObject> playersInGame;

    private void Start()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playersInGame[i].SetActive(true);
        }
    }
}
