using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CastleData",menuName ="ScriptableObject/Agent/CastleData")]
[System.Serializable]
public class CastleData : ScriptableObject
{
    public Sprite sprite;
    public string code;
    public float hp, def;
    public float size;
    public GameObject blood;
    public Action<HeroInfo> extraSkills;
}
