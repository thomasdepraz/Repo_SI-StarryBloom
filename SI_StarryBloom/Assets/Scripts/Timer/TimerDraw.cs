using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDraw : MonoBehaviour
{
    public GameTimer time;
    public TextMeshProUGUI timeLeft;

    public static bool doOnce;
    public static bool doOnceOther;

    void Update()
    {
        int minutes = Mathf.FloorToInt(time.TimeLeft / 60f);
        int seconds = Mathf.FloorToInt(time.TimeLeft % 60f);

        if (minutes == 1 && seconds == 00 && !doOnce)
        {
            Arch.Instance.AppearArks();
            doOnce = true;
        }

        if (minutes == 0 && seconds == 10 && !doOnceOther)
        {
            SoundManager.Instance.PlaySound("SFX_Timer", false);
            LeanTween.scale(timeLeft.transform.parent.gameObject, Vector3.one * 1.2f, 0.2f).setLoopPingPong(25);
            doOnceOther = true;
        }
        timeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
