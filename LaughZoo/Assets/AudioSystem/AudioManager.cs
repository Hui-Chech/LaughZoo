using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void PlayAudio(AudioClip clip, float volume = 1)
    {
        GameObject audioObject = new GameObject();
        AudioSource source = audioObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;
        source.Play();
    }
}
