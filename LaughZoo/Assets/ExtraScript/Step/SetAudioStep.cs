using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioStep : Step
{
    AudioClip clip;
    bool isFinsh = false;
    public SetAudioStep(AudioClip audioClip)
    {
        this.clip = audioClip;
    }
    public override void OnEnter()
    {
        BGMSystem.SetMusic(clip);
        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}

public class SetAudioSpeedStep : Step
{
    float speed;
    bool isFinsh = false;
    public SetAudioSpeedStep(float speed)
    {
        this.speed = speed;
    }
    public override void OnEnter()
    {
        BGMSystem.SetSpeed(speed);
        BackGround.SetSpeed(speed);

        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}
