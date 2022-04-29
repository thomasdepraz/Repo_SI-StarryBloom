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
        if (minutes == 0 && seconds == 10)
            SoundManager.Instance.PlaySound("SFX_Timer", false);
        timeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
