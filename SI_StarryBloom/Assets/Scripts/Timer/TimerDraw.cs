using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDraw : MonoBehaviour
{
    public GameTimer time;

    public TextMeshProUGUI timeLeft;

    void Update()
    {
        int minutes = Mathf.FloorToInt(time.TimeLeft / 60f);
        int seconds = Mathf.FloorToInt(time.TimeLeft % 60f);
        timeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
