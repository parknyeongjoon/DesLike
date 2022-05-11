using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/Agent/HeroData")]
[System.Serializable]
public class HeroData : CastleData
{
    public string soldier_name;
    public float mp, speed, hp_Re;
    public Kingdom kingdom; // 추가 by 시후, 배틀노드 reward 시 필요
    public Tribe tribe;
}