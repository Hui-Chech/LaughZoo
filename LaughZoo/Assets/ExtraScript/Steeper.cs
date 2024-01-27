using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steeper : Step
{
    private List<Step> steps = new List<Step>();
    private int index = 0;
    private int currentIndex = 0;

    /// <summary>
    /// 開始執行
    /// </summary>
    public void OnStart()
    {
        currentIndex = index;
        steps[currentIndex].OnEnter();
    }

    /// <summary>
    /// 添加步驟
    /// </summary>
    /// <param name="step"></param>
    public void AddStep(Step step)
    {
        steps.Add(step);
    }

    /// <summary>
    /// 清除步驟
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
