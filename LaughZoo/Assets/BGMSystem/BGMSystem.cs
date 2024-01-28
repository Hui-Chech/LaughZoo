using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSystem : MonoBehaviour
{
    static BGMSystem instance;

    AudioSource audio;

    void OnDestroy()
    {
        instance = null;
    }

    static void Initial()
    {
        if (instance != null) return;

        GameObject instanceObject = new GameObject("BGM System");
        DontDestroyOnLoad(instanceObject);

        instance = instanceObject.AddComponent<BGMSystem>();


        instance.audio = instanceObject.AddComponent<AudioSource>();
        instance.audio.loop = true;
    }
    internal static void SetMusic(AudioClip music)
    {
        Initial();

        instance.Instance_SetMusic(music);
    }
    public static void SetSpeed(float speed)
    {
        Initial();

        instance.Instance_SetSpeed(speed);
    }

    void Instance_SetMusic(AudioClip music, bool reset = true)
    {
        audio.clip = music;
        audio.volume = 0.2f;
        audio.Play();

        if(reset)
        {
            Instance_SetSpeed(1);
        }
    }
    void Instance_SetSpeed(float speed)
    {
        audio.pitch = speed;
    }
}
