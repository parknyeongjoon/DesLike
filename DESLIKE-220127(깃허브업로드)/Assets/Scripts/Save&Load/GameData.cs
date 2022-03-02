using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroSaveData
{
    public HeroData heroData;
    public float cur_Hp;
    public float cur_Mp;
    public bool resurrection;
    public int curDay;  // 추가(by 시후), 맵 날짜 체크용
}

[System.Serializable]
public class SoldierSaveData
{
    public string soldierCode;
    public GameObject mutant;
    public int remain;
}

[System.Serializable]
public class GameData
{
    public bool canContinue;
    public GoodsCollection goodsCollection;//안됨
    public Map map;//안됨
    public PortDatas allyPortDatas;//안됨
    public List<SoldierSaveData> soldierSaveList;
    public HeroSaveData heroSaveData;
    //mutant, 유물, extraSkills 등 바뀐 애들 저장해야함(continue 누르면 복제하는 식으로 하면 될 듯)
}
