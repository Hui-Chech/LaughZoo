using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstanceFaceSprite : MonoBehaviour
{
    static InstanceFaceSprite instance;

    public Material[] spriteMaterials;
    public Material defaultSpriteMaterial;

    static void Initial()
    {
        if (instance != null) return;

        GameObject instanceObj = new GameObject("FaceSpriteInstancer");
        instance = instanceObj.AddComponent<InstanceFaceSprite>();


        instance.spriteMaterials=  Resources.LoadAll<Material>("Material");
        instance.defaultSpriteMaterial = Resources.Load<Material>("Material/Unlit_SpriteBaseShader");
    }

    public static GameObject InstanceSprite(Sprite sprite, float probability, Transform parent)
    {
        Initial();

        return instance.Instance_InstanceSprite(sprite, probability, parent);
    }
    public static Material GetMaterial(float probability)
    {
        return instance.Instance_GetMaterial(probability);
    }

    GameObject Instance_InstanceSprite(Sprite sprite, float probability, Transform parent)
    {
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.parent = parent;
        spriteObj.transform.localPosition = Vector3.zero;

        SpriteRenderer renderer = spriteObj.AddComponent<SpriteRenderer>();
        renderer.material = Instance_GetMaterial(probability);
        renderer.sprite = sprite;


        return spriteObj;
    }

    Material Instance_GetMaterial(float probability)
    {
        if(Random.value > probability)
        {
            return new Material(defaultSpriteMaterial);
        }
        else
        {
            Material rndMaterial = spriteMaterials[Random.Range(0, spriteMaterials.Length)];
            //Debug.Log(rndMaterial);
            return new Material(rndMaterial);
        }
    }
    
}
