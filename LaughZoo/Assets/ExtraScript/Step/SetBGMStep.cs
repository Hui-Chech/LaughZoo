using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBGMStep : Step
{
    AudioClip clip;
    bool isFinsh = false;
    public SetBGMStep(AudioClip audioClip)
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

public class SetBGMSpeedStep : Step
{
    float speed;
    bool isFinsh = false;
    public SetBGMSpeedStep(float speed)
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
