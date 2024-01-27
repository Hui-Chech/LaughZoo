using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Steeper mainSteeper = new Steeper();
    void Start()
    {
        mainSteeper.AddStep(new LoadSceneStep(SceneName.GameMain));
        mainSteeper.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        mainSteeper.OnUpdate();
    }
}
