using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "ScriptableObject/Agent/SoldierData")]
[System.Serializable]
public class SoldierData : HeroData
{
    public Rarity rarity;
    public int needBarrier, unitAmount;
    public Soldier_Type soldier_Type;
    public int soldierNumber;   // 추가 by 시후, map의 순서와 동일해야함.
}