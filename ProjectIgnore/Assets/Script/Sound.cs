using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    [Range(0, 1.0f)]
    public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
