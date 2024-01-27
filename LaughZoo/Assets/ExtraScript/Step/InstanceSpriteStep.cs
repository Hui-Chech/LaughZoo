using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSpriteStep : Step
{
    Sprite sprite;
    Transform parentTrs;

    float probability;
    bool isFinsh;

    /// <summary>
    /// 生成圖片步驟
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="probability"></param>
    /// <param name="parent"></param>
    public InstanceSpriteStep(Sprite sprite, float probability, Transform parent)
    {
        this.sprite = sprite;
        this.probability = probability;
        this.parentTrs = parent;
    }
    public override void OnEnter()
    {
        InstanceFaceSprite.InstanceSprite(sprite, probability, parentTrs);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override bool IsFinsh()
    {
        return isFinsh;
    }
}
