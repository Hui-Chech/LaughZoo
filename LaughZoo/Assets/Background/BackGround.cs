using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    static BackGround instance;
    
    
    [SerializeField] float speed = 1;

    SpriteRenderer renderer;
    Material material;

    float time;

    void Awake()
    {
        instance = this;

        renderer = GetComponent<SpriteRenderer>();
        material = renderer.material;
    }
    void Update()
    {
        time += Time.deltaTime * speed;

        material.SetFloat("_EffectTime", time);
    }

    public static void SetSpeed(float speed)
    {
        instance.speed = speed;
    }
}
