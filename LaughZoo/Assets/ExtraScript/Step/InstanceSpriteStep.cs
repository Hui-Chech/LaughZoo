using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSpriteStep : Step
{
    Sprite sprite;
    Transform parentTrs;

    float probability;
    bool isFinsh;
    PartType type;

    /// <summary>
    /// 生成圖片步驟
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="probability"></param>
    /// <param name="parent"></param>
    public InstanceSpriteStep(Sprite sprite, float probability, Transform parent, PartType partType)
    {
        this.sprite = sprite;
        this.probability = probability;
        this.parentTrs = parent;
        this.type = partType;
    }
    public override void OnEnter()
    {
        GameObject faceSpritePartParent = new GameObject("FaceSpritePartParent");

        var facePart = InstanceFaceSprite.InstanceSprite(sprite, probability, faceSpritePartParent.transform);

        var facePartBase = faceSpritePartParent.AddComponent<FacePartBase>();
        facePartBase.type = type;

        isFinsh = true;
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
