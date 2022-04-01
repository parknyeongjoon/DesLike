using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ConstNum
{
    public const int soldierNum = 45;
    public const int relicNum = 30;
}

[System.Serializable]
public class HeroSaveData
{
    public HeroData heroData;
    public float cur_Hp;
    public float cur_Mp;
    public bool resurrection;
}

[System.Serializable]
public class SoldierSaveData
{
    public string soldierCode;
    public GameObject mutant;
    public int remain;
}

[System.Serializable]
public class IsFind
{
    public bool[] soldierFind = new bool[ConstNum.soldierNum];
    public bool[] relicFind = new bool[ConstNum.relicNum];
}

[System.Serializable]
public class GameOption
{
    public float soundVol;
    public bool storySkip;
}

[System.Serializable]
public class GameData
{
    public bool canContinue;
    public IsFind isFind;
    public GoodsCollection goodsCollection;//�ȵ�
    public Map map;//�ȵ�
    public PortDatas allyPortDatas;//�ȵ�
    public List<SoldierSaveData> soldierSaveList;
    public HeroSaveData heroSaveData;
    //mutant, ����, extraSkills �� �ٲ� �ֵ� �����ؾ���(continue ������ �����ϴ� ������ �ϸ� �� ��)
}
