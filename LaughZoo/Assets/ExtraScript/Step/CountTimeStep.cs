using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTimeStep : Step
{
    private float currentTime = 0;
    private float timer = 0;

    private bool isFinsh;

    public CountTimeStep(float Timer)
    {
        this.timer = Timer;
    }
    public override void OnEnter()
    {
        currentTime = 0;
        isFinsh = false;
    }
    public override void OnUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timer)
        {
            isFinsh = true;
            Debug.Log($"­Ë¼Æ{timer}¬íµ²§ô");
        }
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}
