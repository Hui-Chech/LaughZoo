using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingSprite : MonoBehaviour
{
    [SerializeField] protected float flyingSpeed = 5f;
    [SerializeField] protected float rotateSpeed = 120f;
    [SerializeField] protected float minChangePositionTime = .1f;
    [SerializeField] protected float maxChangePositionTime = .5f;
    [SerializeField] protected float angleRange = 1f;

    protected float nextAngle;
    protected Vector2 fixedForward;
    private Transform spriteTransform;
    private Vector2 border;

    private void Awake()
    {
        InternalInit();
        Init();
    }

    protected virtual void Start()
    {
        fixedForward = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.up;

        StartCoroutine(TargetPositionUpdater());
    }

    private void InternalInit()
    {
        spriteTransform = transform.GetChild(0);
        border = new Vector2(12.5f, 7f);
    }

    public void Init(float flyingSpeed = default, float rotateSpeed = default, float minChangePositionTime = default, float maxChangePositionTime = default , float angleRange = default)
    {
        this.flyingSpeed = flyingSpeed;
        this.rotateSpeed = rotateSpeed;
        this.minChangePositionTime = minChangePositionTime;
        this.maxChangePositionTime = maxChangePositionTime;
        this.angleRange = angleRange;
    }

    protected virtual void Update()
    {
        // Update sprite position
        transform.Translate(flyingSpeed * Time.deltaTime * fixedForward);
        spriteTransform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);

        // Over screen protection
        var pos = transform.position;
        if (Mathf.Abs(pos.x) > border.x)
            pos.x = -Mathf.Sign(pos.x) * border.x;

        if (Mathf.Abs(pos.y) > border.y)
            pos.y = -Mathf.Sign(pos.y) * border.y;

        transform.position = pos;
    }

    protected abstract IEnumerator TargetPositionUpdater();
}
