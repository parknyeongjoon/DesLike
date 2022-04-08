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

public enum CurWindow { Map, Event, Battle }

[System.Serializable]
public class MapData
{
    public Kingdom kingdom;
    public CurWindow curWindow;
    public int curStage = 0;
    public int curTrack;
    public int curDay;  // ���� ��¥
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // ���� �̺�Ʈ(����) �����. 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
    public int[] evntList = new int[3];    // �̺�Ʈ�� � �̺�Ʈ���� ����, 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
    public int curBtn;  // ���� � ��ư�� ������ �� ������ �Դ���
    public bool[] isEventSet = new bool[3];
    public bool[] isRewardSet = new bool[3];
    public bool[] isAbleSet = new bool[3];
    public bool[] isChallenge = new bool[3];
    public int challengeCount = 0;
    public bool midBossCheck1, midBossCheck2, villageCheck, organCheck, newSet; // �߰� ����, ����, ����, �̹� �����ߴ��� ����
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
    //mutant, ����, extraSkills �� �ٲ� �ֵ� �����ؾ���(continue ������ �����ϴ� ������ �ϸ� �� ��)
}
