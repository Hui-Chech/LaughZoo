using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSpriteTesting : MonoBehaviour
{

    public Sprite testSprite;

    [Range(0, 1f)]
    public float probability = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstanceFaceSprite.InstanceSprite(testSprite, probability, transform);
        }
    }
}
