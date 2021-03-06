using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [Header("Clock")]
    [SerializeField] float gameDuration = 5f * 60f;
    public IEnumerator clockSpendTime;
    [field: SerializeField] public float TimeLeft { get; private set; }

    public UnityEvent onTimerEnd;
    public GameObject timerUI;

    public void RebootTimer()
    {
        //Coroutine
        if (clockSpendTime != null) StopCoroutine(clockSpendTime);
        clockSpendTime = ResetClock(gameDuration);
        StartCoroutine(clockSpendTime);
    }

    private IEnumerator ResetClock(float duration)
    {
        TimeLeft = duration;
        while (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            yield return null;
        }
        TimeLeft = 0.0f;
        onTimerEnd?.Invoke();
    }


}
