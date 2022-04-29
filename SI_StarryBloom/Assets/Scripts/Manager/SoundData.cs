using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
}
