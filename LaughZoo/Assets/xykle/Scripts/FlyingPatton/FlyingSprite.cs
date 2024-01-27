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

    protected virtual void Start()
    {
        spriteTransform = transform.GetChild(0);

        fixedForward = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.up;
        StartCoroutine(TargetPositionUpdater());
    }

    private void Update()
    {
        // Update sprite position
        transform.Translate(flyingSpeed * Time.deltaTime * fixedForward);
        spriteTransform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
    }

    protected abstract IEnumerator TargetPositionUpdater();
}
