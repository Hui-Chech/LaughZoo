using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Debug.Log($"倒數{timer}秒結束");
        }
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}

public class FinshStep : Step
{
    private bool isFinsh;
    public override void OnEnter()
    {
        var player = GameObject.FindObjectOfType<PlayerControl>();

        player.isGameStarted = false;

        var finshIndex = player.GetFinshIndex;

        Debug.Log($"完成{finshIndex}次");

        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}

public class ShowEndBackGround : Step
{
    bool isFinsh;
    public override void OnEnter()
    {
        var endBackGround = GameObject.FindObjectOfType<GetEndCanvas>(true);
        endBackGround.gameObject.SetActive(true);

        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}

public class RestartGameStep : Step
{
    bool isFinsh;
    public override void OnEnter()
    {
        var btn = GameObject.FindObjectOfType<GetEndCanvas>(true).GetResetBtn();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        SceneManager.UnloadSceneAsync(SceneName.GameMain);
        SceneManager.UnloadSceneAsync(0);

        SceneManager.LoadSceneAsync(0);

        isFinsh = true;
    }

    public override bool IsFinsh()
    {
        return isFinsh;
    }
}