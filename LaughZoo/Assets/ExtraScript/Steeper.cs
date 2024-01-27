using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steeper : Step
{
    private List<Step> steps = new List<Step>();
    private int index = 0;
    private int currentIndex = 0;

    /// <summary>
    /// �}�l����
    /// </summary>
    public void OnStart()
    {
        currentIndex = index;
        steps[currentIndex].OnEnter();
    }

    /// <summary>
    /// �K�[�B�J
    /// </summary>
    /// <param name="step"></param>
    public void AddStep(Step step)
    {
        steps.Add(step);
    }

    /// <summary>
    /// �M���B�J
    /// </summary>
    public void Clear()
    {
        steps.Clear();
    }


    public override void OnEnter()
    {
        index = currentIndex;
        steps[currentIndex].OnEnter();
    }

    public override void OnUpdate()
    {
        if (IsFinsh()) return;

        if (index != currentIndex)
        {
            OnEnter();
        }

        steps[currentIndex].OnUpdate();

        if (steps[currentIndex].IsFinsh())
        {
            currentIndex++;
        }
    }
    public override bool IsFinsh()
    {
        return currentIndex >= steps.Count;
    }
}
