using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritePartLibrary", menuName = "New SpritePartLibrary", order = 1)]
public class SpriteManager : ScriptableObject
{
    public List<SpriteList> FaceParts = new List<SpriteList>();
}

[System.Serializable]
public class SpriteList
{
    public string AnimalName;
    public List<Sprite> EyeLeft = new List<Sprite>();
    public List<Sprite> EyeRight = new List<Sprite>();
    public List<Sprite> Nose = new List<Sprite>();
    public List<Sprite> Mouth = new List<Sprite>();
}