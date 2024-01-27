using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritePartLibrary", menuName = "New SpritePartLibrary", order = 1)]
public class SpriteManager : ScriptableObject
{
    public SpriteList faceParts = new SpriteList();
}

[System.Serializable]
public class SpriteList
{
    public string Name;
    public List<Sprite> EyeLeft;
    public List<Sprite> EyeRight;
    public List<Sprite> Nose;
    public List<Sprite> Mouth;
}