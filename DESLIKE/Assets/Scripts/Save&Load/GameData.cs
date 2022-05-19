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
    public int areaGold = 0;
}

[System.Serializable]
public class IsFind//false�� �ʱ�ȭ
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

[System.Serializable]
public class MapData
{
    public Kingdom kingdom;
    public CurWindow curWindow = CurWindow.Map;
    public CurBattle curBattle = CurBattle.Normal;
    public int curStage = 0;
    public int curTrack = 0;
    public int curDay = 0;  // ���� ��¥
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // ���� �̺�Ʈ(����) �����. 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
    public int[] evntList = new int[3];    // �̺�Ʈ�� � �̺�Ʈ���� ����, 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
    public int curBtn = 0;  // ���� � ��ư�� ������ �� ������ �Դ���
    public bool[] isEventSet = new bool[3];
    public bool[] isRewardSet = new bool[3];
    public bool[] isAbleSet = new bool[3];
    public bool[] isChallenge = new bool[3];
    public int challengeCount = 0;
    public bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false, 
        newSet = false, eventEnd = false; // �߰� ����, ����, ����, �̹� �����ߴ��� ����
}

[System.Serializable]
public class EventData
{
    public int ranPenalty, rewardNum, comBox;
    public bool isEventSet, isAlreadySelect;
    public int[] optionNum = new int[3];
    public int[] areaGold = new int[3];
    public int[] choiNum = new int[3];    // 0 : 1��° ������(���, ü��, X), 1 : 2��° ������(�׼�), 2 : ���� ����
    public bool[] stepCheck = new bool[5];
}

[System.Serializable]
public class VillageData
{
    public int[] randRelic = new int[6];   // ��Ϻ� �����ѹ�
    public int[] relicPrice = new int[6];  // ��Ϻ� ����
    public bool[] isSoldOut = new bool[6];
    public bool isNewSet;
    public int healCount;
}


[System.Serializable]
public class CurBattleNodeData
{
    public string[,] ableSoldierIndex;  // [������, ����]
    public string[,] ableRelicIndex;    // [������, ����]

    public string[,] solRewardIndex = new string[3,2];
    public int[,] relRewardIndex;   // int���� string ���� �ʿ�

    public int curEnemyPortNum;
    public int eventSetNum;
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
    public List<string> relicSaveData = new List<string>();
    public EventData eventData = new EventData();
    public MapData mapData = new MapData();
    public VillageData villageData = new VillageData();
    public CurBattleNodeData curBattleNodeData = new CurBattleNodeData();
    //mutant, ����, extraSkills �� �ٲ� �ֵ� �����ؾ���(continue ������ �����ϴ� ������ �ϸ� �� ��)
}
