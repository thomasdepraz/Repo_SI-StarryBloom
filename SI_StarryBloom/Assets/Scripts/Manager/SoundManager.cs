using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public SoundData soundDatabase;
    public List<AudioSource> audioSources = new List<AudioSource>();
    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private void Awake()
    {
        CreateSingleton(true);
    }

    public void Start()
    {
        //Fill sounds dictionary
        for (int i = 0; i < soundDatabase.clips.Count; i++)
        {
            sounds[soundDatabase.clips[i].name] = soundDatabase.clips[i];
        }
    }

    public void PlaySound(string soundName, bool looping)
    {
        var clip = sounds[soundName];
        if (clip == null) return;


        var source = GetAudioSource();
        source.clip = clip;
        source.loop = looping;
        source.Play();
    }

    public AudioSource GetAudioSource()
    {
        AudioSource source = null;
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                source = audioSources[i];
                break;
            }
        }

        if (source == null)
            source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        return source;
    }

}

[CreateAssetMenu(fileName = "Sound Data", menuName = "Data/Sound Data")]
public class SoundData : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
}
