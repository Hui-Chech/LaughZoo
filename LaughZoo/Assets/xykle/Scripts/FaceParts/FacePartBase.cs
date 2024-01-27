using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(FlyingSprite))]
public abstract class FacePartBase : MonoBehaviour
{
    public abstract PartType type { get; protected set; }
    private Collider2D _collider;
    private FlyingSprite _flyingSprite;

    protected virtual void Start()
    {
        InternalInit();
    }

    private void InternalInit()
    {
        _collider = GetComponent<Collider2D>();
        _flyingSprite = GetComponent<FlyingSprite>();

        _collider.isTrigger = true;
    }

    public virtual PartType PartSmashed(Transform parent)
    {
        _collider.enabled = false;
        _flyingSprite.enabled = false;

        transform.parent = parent;

        return type;
    }
}

public enum PartType
{
    Eye_L,
    Eye_R,
    Nose,
    Mouth,
}