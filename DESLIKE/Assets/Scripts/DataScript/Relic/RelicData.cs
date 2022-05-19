using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicData : ScriptableObject
{
    public Sprite relicImg;
    public string code = "";
    public string relicName;
    public string toopTip;
    public Kingdom kingdom; // 추가 by 시후, savemanager 시 필요
    public Rarity rarity;
    public bool continueReuse;
}
