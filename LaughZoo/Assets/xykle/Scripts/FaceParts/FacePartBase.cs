using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(RandomFly))]
public class FacePartBase : MonoBehaviour
{
    public PartType type;
    private Collider2D _collider;
    private FlyingSprite _flyingSprite;

    protected virtual void Start()
    {
        InternalInit();
    }

    private void InternalInit()
    {
        _collider = GetComponent<Collider2D>();

        _collider.isTrigger = true;
    }

    public virtual PartType PartSmashed(Transform parent)
    {
        _collider.enabled = false;
        GetComponent<FlyingSprite>().enabled = false;

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