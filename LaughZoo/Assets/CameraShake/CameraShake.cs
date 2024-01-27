using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static CameraShake instance;

    void Start()
    {
        instance = this;
    }


    public static void ShakeCamera()
    {
        ShakeCamera(0.05f, Random.onUnitSphere);
    }
    public static void ShakeCamera(float duration, Vector3 direction)
    {
        instance.StartCoroutine(instance.Instance_ShakeCamera(duration, direction));
    }

    IEnumerator Instance_ShakeCamera(float duration, Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;

        float t = 0;
        while (t < 1)
        {
            t += (Time.deltaTime) / duration;

            float lerpT = Mathf.PingPong(t * 2, 1);
            transform.position = Vector3.Lerp(startPosition, targetPosition, lerpT);

            yield return null;
        }

        transform.position = startPosition;
    }


}
