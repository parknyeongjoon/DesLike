using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicData", menuName = "ScriptableObject/RelicData")]
public class RelicData : ScriptableObject
{
    public Sprite relicImg;
    public string code = "";
    public string relicName;
    public string toopTip;
    public Rarity rarity;
    public bool continueReuse;
}
