using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        SoundManager.instance = this;
    }


    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName == name)
            {
                s.source.Play();
                Debug.Log("Play");
                break;
            }
        }
    }
}

