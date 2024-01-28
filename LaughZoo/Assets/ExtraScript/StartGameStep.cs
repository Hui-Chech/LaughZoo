using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameStep : Step
{
    bool isFinsh;
    //GameObject startCanvasObj;

    //private Steeper steeper = new Steeper();
    //AudioClip StartBGM;
    //AudioClip GameBGM;

    //public StartGameStep(AudioClip StartBGM, AudioClip GameBgm)
    //{

    //}
    //public override void OnEnter()
    //{
    //    steeper.AddStep(new SetBGMStep());
    //    steeper.AddStep(new ShowStartCanvas());
    //    steeper.AddStep(new SetBGMStep());
    //}
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}

public class ShowStartCanvas : Step
{
    GameObject startCanvasObj;
    bool isFinsh;
    public override void OnEnter()
    {
        var startCanvas = GameObject.FindObjectOfType<GetstartCanvas>(true);
        startCanvasObj = startCanvas.gameObject;
        startCanvasObj.SetActive(true);

        var startButton = startCanvas.GetGameStartButton();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartGame);
    }
    public void StartGame()
    {
        startCanvasObj.SetActive(false);
        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }
}