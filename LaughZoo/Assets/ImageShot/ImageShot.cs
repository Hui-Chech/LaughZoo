using System;
using System.Collections;
using System.Collections.Generic;
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


    public static void ShotImage(GameObject target, float range)
    {
        CaptureImage(target.transform.position, range);
    }
    public static void CaptureImage(Vector2 position, float range)
    {
        Initial();

        instance.SetCaemra(position, range);
        instance.SaveCameraView();
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
    void SaveCameraView()
    {
        RenderTexture screenTexture = new RenderTexture(resoluction.x, resoluction.y, 0);
        camera.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;

        camera.Render();

        Texture2D renderedTexture = new Texture2D(resoluction.x, resoluction.y);
        renderedTexture.ReadPixels(new Rect(0, 0, resoluction.x, resoluction.y), 0, 0);
        RenderTexture.active = null;

        byte[] byteArray = renderedTexture.EncodeToPNG();

        string folderPath = Application.dataPath + "/Capture";
        string imageName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        System.IO.Directory.CreateDirectory(folderPath);
        File.WriteAllBytes($"{folderPath}/{imageName}.png", byteArray);
    }
}
