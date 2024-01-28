using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private SpriteManager spriteManager;

    private Steeper mainSteeper = new Steeper();

    /// <summary>
    /// 計時控制步驟
    /// </summary>
    private Steeper timerSteeper = new Steeper();

    /// <summary>
    /// 音量控制步驟
    /// </summary>
    public Steeper audioSteeper = new Steeper();

    /// <summary>
    /// 部件生成步驟
    /// </summary>
    public Steeper partSteeper = new Steeper();

    private List<Sprite> eyeLeft = new List<Sprite>();
    private List<Sprite> eyeRight = new List<Sprite>();
    private List<Sprite> mouth = new List<Sprite>();
    private List<Sprite> nose = new List<Sprite>();
    private List<Sprite> eyebrowLeft = new List<Sprite>();
    private List<Sprite> eyebrowRight = new List<Sprite>();

    private void Init()
    {
        if (spriteManager == null) return;

        foreach (Sprite sprite in spriteManager.faceParts.EyeLeft)
        {
            eyeLeft.Add(sprite);
        }

        foreach (Sprite sprite in spriteManager.faceParts.EyeRight)
        {
            eyeRight.Add(sprite);
        }

        foreach (Sprite sprite in spriteManager.faceParts.Mouth)
        {
            mouth.Add(sprite);
        }

        foreach (Sprite sprite in spriteManager.faceParts.Nose)
        {
            nose.Add(sprite);
        }
        foreach (Sprite sprite in spriteManager.faceParts.EyebrowLeft)
        {
            eyebrowLeft.Add(sprite);
        }
        foreach (Sprite sprite in spriteManager.faceParts.EyebrowRight)
        {
            eyebrowRight.Add(sprite);
        }
    }
    void Start()
    {
        SetAudioSteeper();

        SetLoopSteeper();

        mainSteeper.AddStep(new LoadSceneStep(SceneName.GameMain));

        mainSteeper.AddStep(new CountTimeStep(60f));

        mainSteeper.AddStep(new FinshStep());

        mainSteeper.OnStart();
    }

    void Update()
    {
        //mainSteeper.OnUpdate();
        audioSteeper.OnUpdate();

        partSteeper.OnUpdate();

        mainSteeper.OnUpdate();

        if (mainSteeper.IsFinsh())
        {
            Debug.Log("Game over");
        }
    }
    Sprite GetRandomSprite(List<Sprite> sprites)
    {
        Random.Range(0, sprites.Count - 1);

        var curentSprite = sprites[Random.Range(0, sprites.Count - 1)];

        sprites.Remove(curentSprite);

        return curentSprite;
    }

    public void SetAudioSteeper()
    {
        audioSteeper.AddStep(new SetAudioStep(spriteManager.faceParts.BGM));
        audioSteeper.AddStep(new CountTimeStep(20));
        audioSteeper.AddStep(new SetAudioSpeedStep(1.25f));
        audioSteeper.AddStep(new CountTimeStep(20));
        audioSteeper.AddStep(new SetAudioSpeedStep(1.5f));

        audioSteeper.OnStart();
    }

    public void SetPartSteeper()
    {
        partSteeper.AddStep(new CountTimeStep(4));

        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeLeft), 1, transform, PartType.Eye_L));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeRight), 1, transform, PartType.Eye_R));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(mouth), 1, transform, PartType.Mouth));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(nose), 1, transform, PartType.Nose));
    }

    void InstanceSpriteStep()
    {
        Init();

        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeRight), Random.Range(0, 1), transform, PartType.Eye_R));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeLeft), Random.Range(0, 1), transform, PartType.Eye_L));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(mouth), Random.Range(0, 1), transform, PartType.Mouth));
        partSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(nose), Random.Range(0, 1), transform, PartType.Nose));
    }

    public void SetLoopSteeper()
    {
        InstanceSpriteStep();

        for (int i = 0; i < 5; i++)
        {
            partSteeper.AddStep(new CountTimeStep(4));//4

            InstanceSpriteStep();
        }

        for (int i = 0; i < 6; i++)
        {
            partSteeper.AddStep(new CountTimeStep(3));//23

            InstanceSpriteStep();
        }

        for (int i = 0; i < 10; i++)
        {
            partSteeper.AddStep(new CountTimeStep(2));//23

            InstanceSpriteStep();
        }

        partSteeper.OnStart();
    }
}
