using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class RandomFly : FlyingSprite
{
    protected override IEnumerator TargetPositionUpdater()
    {
        float waitTime = Random.Range(minChangePositionTime, maxChangePositionTime);
        nextAngle = Random.Range(-angleRange, angleRange);
        var timer = 0f;
        var currentAngle = 0f;
        
        while (timer < waitTime)
        {
            currentAngle = Mathf.Lerp(currentAngle, nextAngle, timer / waitTime);

            fixedForward = Quaternion.AngleAxis(currentAngle, Vector3.forward) * fixedForward;

            yield return null;
            timer += Time.deltaTime;
        }

        StartCoroutine(TargetPositionUpdater());
    }
}
