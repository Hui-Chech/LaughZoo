using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSpriteTesting : MonoBehaviour
{

    [Space]
    public Sprite testSprite;
    [Range(0, 1f)] public float probability = 0;

    [Space]
    public GameObject shotTarget;

    [Space]
    public AudioClip clip;

    [Space]
    public AudioClip bgmA;
    public AudioClip bgmB;
    [Range(1, 3)] public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            InstanceFaceSprite.InstanceSprite(testSprite, probability, transform);
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            ImageShot.ShotImage(shotTarget, 30);
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            AudioManager.PlayAudio(clip);
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            BGMSystem.SetMusic(bgmA);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            BGMSystem.SetMusic(bgmB);
        }

        BGMSystem.SetSpeed(speed);
    }
}
