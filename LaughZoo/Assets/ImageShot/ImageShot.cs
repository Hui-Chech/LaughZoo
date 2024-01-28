using System;
using System.IO;
using UnityEngine;

public class ImageShot : MonoBehaviour
{
    static ImageShot instance;

    [SerializeField] Vector2Int resoluction = new Vector2Int(1024, 1024);

    Camera camera;

    void OnDestroy()
    {
        instance = null;
    }


    public static Sprite ShotImage(GameObject target, float range, LayerMask mask)
    {
        return CaptureImage(target.transform.position, range, mask);
    }
    public static Sprite CaptureImage(Vector2 position, float range, LayerMask mask)
    {
        Initial();

        instance.SetCaemra(position, range);
        Texture2D savedTexture = instance.SaveCameraView(mask);

        return Sprite.Create(savedTexture, new Rect(0.0f, 0.0f, savedTexture.width, savedTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    static void Initial()
    {
        if (instance != null) return;

        GameObject instanceObj = new GameObject("Image Shot");

        instance = instanceObj.AddComponent<ImageShot>();
        instance.camera = instanceObj.AddComponent<Camera>();

        instance.camera.orthographic = true;
    }

    void SetCaemra(Vector3 position, float size)
    {
        camera.transform.position = position + Vector3.back * 20;
        camera.orthographicSize = size;
    }
    Texture2D SaveCameraView(LayerMask mask)
    {
        RenderTexture screenTexture = new RenderTexture(resoluction.x, resoluction.y, 0);
        camera.targetTexture = screenTexture;
        camera.cullingMask = mask;
        RenderTexture.active = screenTexture;

        camera.Render();

        Texture2D renderedTexture = new Texture2D(resoluction.x, resoluction.y);
        renderedTexture.ReadPixels(new Rect(0, 0, resoluction.x, resoluction.y), 0, 0);
        renderedTexture.Apply();
        RenderTexture.active = null;

        byte[] byteArray = renderedTexture.EncodeToPNG();

        string folderPath = Application.dataPath + "/Capture";
        string imageName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        System.IO.Directory.CreateDirectory(folderPath);
        File.WriteAllBytes($"{folderPath}/{imageName}.png", byteArray);

        return renderedTexture;
    }
}
