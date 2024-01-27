using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingSprite : MonoBehaviour
{
    [SerializeField] protected float flyingSpeed;
    [SerializeField] protected float rotateSpeed;
    [SerializeField] protected float minChangePositionTime;
    [SerializeField] protected float maxChangePositionTime;
    [SerializeField] protected float angleRange;

    protected float nextAngle;
    protected Vector2 fixedForward;
    private Transform spriteTransform;
    private Vector2 border;

    protected virtual void Start()
    {
        Init();

        fixedForward = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.up;

        StartCoroutine(TargetPositionUpdater());
    }

    private void Init()
    {
        spriteTransform = transform.GetChild(0);
        border = new Vector2(12.5f, 7f);
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
