using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadpictureStep : Step
{
    string folderPath = Application.dataPath + "/Capture";

    bool isFinsh;
    public override void OnEnter()
    {
        if (Directory.Exists(folderPath))
        {
            string[] pngFiles = Directory.GetFiles(folderPath,"*.png");

            foreach (string pngFile in pngFiles)
            {
                byte[] fileData = File.ReadAllBytes(pngFile);

                Texture2D texture = new Texture2D(1024, 1024);
                texture.LoadImage(fileData);

                GameObject currentPicture = new GameObject("SavedPicture");
                currentPicture.transform.parent = GameObject.FindObjectOfType<GetEndCanvas>().GetFinshGroupRoot();
                var currentPictureImage = currentPicture.AddComponent<Image>();
                currentPictureImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            }
        }
        isFinsh = true;
    }
    public override bool IsFinsh()
    {
        return isFinsh;
    }

}
