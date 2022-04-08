using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ConstNum
{
    public const int soldierNum = 45;
    public const int relicNum = 30;
    public const int stroyNum = 3;
}

[System.Serializable]
public class HeroSaveData
{
    public string heroCode = "";
    public float cur_Hp = 0;
    public float cur_Mp = 0;
    public bool resurrection = false;
}

[System.Serializable]
public class PortSaveData
{
    public bool isUnlock = false;
    public string soldierCode ="";
    public string mutantCode ="";
}

[System.Serializable]
public class PortSaveDatas
{
    public PortSaveData[] portSaveList = new PortSaveData[30];
    public int curBarrierStrength = 0, maxBarrierStrength = 0;
}

[System.Serializable]
public class GoodsSaveData
{
    public int gold = 0;
    public int magicalStone = 0;
}

[System.Serializable]
public class IsFind//false로 초기화
{
    public bool[] soldierFind = new bool[ConstNum.soldierNum];
    public bool[] relicFind = new bool[ConstNum.relicNum];
    public bool[] stroyFind = new bool[ConstNum.stroyNum];
}

public class GameOption
{
    public float soundVol = 100;
    public bool storySkip = false;
    public bool autoBattleStart = false;
}

public enum CurWindow { Map, Event, Battle }

[System.Serializable]
public class MapData
{
    public Kingdom kingdom;
    public CurWindow curWindow;
    public int curStage = 0;
    public int curTrack;
    public int curDay;  // 현재 날짜
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public int[] evntList = new int[3];    // 이벤트가 어떤 이벤트인지 저장, 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public int curBtn;  // 현재 어떤 버튼을 눌러서 이 곳으로 왔는지
    public bool[] isEventSet = new bool[3];
    public bool[] isRewardSet = new bool[3];
    public bool[] isAbleSet = new bool[3];
    public bool[] isChallenge = new bool[3];
    public int challengeCount = 0;
    public bool midBossCheck1, midBossCheck2, villageCheck, organCheck, newSet; // 중간 보스, 마을, 정비, 이미 세팅했는지 여부
}

[System.Serializable]
public class RewardData
{
    public int[] relicRewardIndex = new int[3];
    public int[,] soldierRewardIndex = new int[2, 3];
    }

[System.Serializable]
public class GameData
{
    public GameOption gameOption;
    public IsFind isFind;
    public bool canContinue;
    public HeroSaveData heroSaveData = new HeroSaveData();
    public PortSaveDatas portSaveDatas = new PortSaveDatas();
    public GoodsSaveData goodsSaveData = new GoodsSaveData();
    public MapData mapData;
    public RewardData rewardData;
    //mutant, 유물, extraSkills 등 바뀐 애들 저장해야함(continue 누르면 복제하는 식으로 하면 될 듯)
}
