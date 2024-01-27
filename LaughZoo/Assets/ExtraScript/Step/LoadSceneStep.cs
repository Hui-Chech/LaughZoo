using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneStep : Step
{
    AsyncOperation operation;
    string sceneName;

    bool isFinsh;

    public LoadSceneStep(string sceneName)
    {
        this.sceneName = sceneName;
    }
    public override void OnEnter()
    {
        isFinsh = false;
        operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public override void OnUpdate()
    {
        if (operation.isDone)
        {
            isFinsh = operation.isDone;
        }
    }

    public override bool IsFinsh()
    {
        return isFinsh;
    }
}
