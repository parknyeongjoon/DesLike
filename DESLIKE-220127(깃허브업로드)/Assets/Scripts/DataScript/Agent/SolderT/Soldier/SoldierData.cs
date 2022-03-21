using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "ScriptableObject/Agent/SoldierData")]
[System.Serializable]
public class SoldierData : HeroData
{
    public Rarity rarity;
    public int cost, unitAmount;
    public Soldier_Type soldier_Type;
}
