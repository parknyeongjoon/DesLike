using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicData : ScriptableObject
{
    public Sprite relicImg;
    public string code = "";
    public string relicName;
    public string toopTip;
    public Kingdom kingdom; // �߰� by ����, savemanager �� �ʿ�
    public Rarity rarity;
    public bool continueReuse;

    public virtual void Effect(HeroData heroData)
    {

    }

    public virtual bool ConditionCheck(HeroData heroData)
    {
        return false;
    }
}
