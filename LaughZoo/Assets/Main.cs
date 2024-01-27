using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private SpriteManager spriteManager;

    private Steeper mainSteeper = new Steeper();

    private List<Sprite> eyeLeft = new List<Sprite>();
    private List<Sprite> eyeRight = new List<Sprite>();
    private List<Sprite> mouth = new List<Sprite>();
    private List<Sprite> nose = new List<Sprite>();
    void Start()
    {
        Init();

        mainSteeper.AddStep(new LoadSceneStep(SceneName.GameMain));

        mainSteeper.AddStep(new CountTimeStep(3));

        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeLeft), 1, transform)); 
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeRight), 1, transform)); 
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(mouth), 1, transform)); 
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(nose), 1, transform));

        mainSteeper.AddStep(new CountTimeStep(4));

        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeLeft), 1, transform));
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(eyeRight), 1, transform));
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(mouth), 1, transform));
        mainSteeper.AddStep(new InstanceSpriteStep(GetRandomSprite(nose), 1, transform));


        mainSteeper.OnStart();
    }

    private void Init()
    {
        if (spriteManager == null) return;

        eyeLeft = spriteManager.faceParts.EyeLeft;
        eyeRight = spriteManager.faceParts.EyeRight;
        mouth = spriteManager.faceParts.Mouth;
        nose = spriteManager.faceParts.Nose;
    }
    void Update()
    {
        mainSteeper.OnUpdate();
    }
    Sprite GetRandomSprite(List<Sprite> sprites)
    {
        Random.Range(0, sprites.Count - 1);

        var curentSprite = sprites[Random.Range(0, sprites.Count - 1)];

        sprites.Remove(curentSprite);

        return curentSprite;
    }
}
