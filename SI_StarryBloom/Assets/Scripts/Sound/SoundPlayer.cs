using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        if (SoundManager.Instance == null) return;

        SoundManager.Instance.PlaySound(soundName, false);
    }
}
