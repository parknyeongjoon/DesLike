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
public class MapData
{
    public int curStage = 0;
    public int curTrack;
    public int curDay;  // ���� ��¥
    public int curWindow;   // ���� â�� ��(0), �̺�Ʈ(1), ����(2)����
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // ���� �̺�Ʈ(����) �����. 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
    public bool[] isEventSet = new bool[3];
    public bool[] isRewardSet = new bool[3];
    public bool[] isChallenge = new bool[3];
    public int challengeCount = 0;
    public bool midBossCheck1, midBossCheck2, villageCheck, organCheck, newSet; // �߰� ����, ����, ����, �̹� �����ߴ��� ����
}

[System.Serializable]
public class RewardData
{
    public int[] relicRewardIndex = new int[3];
    public int[] soldierRewardIndex = new int[3];
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
    public MapData mapData;
    public RewardData rewardData;
    //mutant, ����, extraSkills �� �ٲ� �ֵ� �����ؾ���(continue ������ �����ϴ� ������ �ϸ� �� ��)
}
