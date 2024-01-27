using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritePartLibrary", menuName = "New SpritePartLibrary", order = 1)]
public class SpriteManager : ScriptableObject
{
    public SpriteList spriteList = new SpriteList();
}

[System.Serializable]
public class SpriteList
{
    public List<Sprite> sprites = new List<Sprite>();
}