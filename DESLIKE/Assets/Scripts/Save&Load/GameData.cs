using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

static class ConstNum
{
    public const int soldierNum = 45;
    public const int relicNum = 30;
    public const int stroyNum = 3;
}

[System.Serializable]
public class HeroSaveData
{
    public string heroCode;
    public float cur_Hp;
    public float cur_Mp;
    public bool resurrection;
}

[System.Serializable]
public class SoldierSaveData
{
    public string soldierCode;
    public string mutantCode;
    public int portNum;
    public int remain;
}

[System.Serializable]
public class IsFind
{
    public bool[] soldierFind = new bool[ConstNum.soldierNum];
    public bool[] relicFind = new bool[ConstNum.relicNum];
    public bool[] stroyFind = new bool[ConstNum.stroyNum];
}

public class GameOption
{
    public float soundVol;
    public bool storySkip;
    public bool autoBattleStart;
}

public enum curWindow { Map, Event, Battle }

[System.Serializable]
public class MapData
{
    public int curStage = 0;
    public int curTrack;
    public int curDay;  // 현재 날짜
    public int curWindow;   // 현재 창이 맵(0), 이벤트(1), 전투(2)인지
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public int[] evntList = new int[3];    // 이벤트가 어떤 이벤트인지 저장, 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public int curBtn;  // 현재 어떤 버튼을 눌러서 이 곳으로 왔는지
    public bool[] isEventSet = new bool[3];
    public bool[] isRewardSet = new bool[3];
    public bool[] isChallenge = new bool[3];
    public int challengeCount = 0;
    public bool midBossCheck1, midBossCheck2, villageCheck, organCheck, newSet; // 중간 보스, 마을, 정비, 이미 세팅했는지 여부
}

[System.Serializable]
public class RewardData
{
    public int[] relicRewardIndex = new int[3];
    public int[] soldierRewardIndex = new int[3];
}

[System.Serializable]
public class SaveData//게임 껐다 켰을 때 데이터 저장용 클래스들. 수시로 저장해주고 불러오는 건 게임 continue 누를 때만 해주면 됨
{
    public HeroSaveData heroSaveData;
    public List<string> activeSoldierList;

    public void SaveActiveSoldierList(List<string> _activeSoldierList)
    {
        for(int i = 0; i < _activeSoldierList.Count; i++)
        {

        }
    }

    public void SaveHeroData(HeroInfo heroInfo)
    {
        heroSaveData.heroCode = heroInfo.castleData.code;
        heroSaveData.cur_Hp = heroInfo.cur_Hp;
        heroSaveData.cur_Mp = heroInfo.cur_Mp;
        heroSaveData.resurrection = heroInfo.resurrection;
    }
}

[System.Serializable]
public class GameData
{
    public bool canContinue;
    public IsFind isFind;
    public GoodsCollection goodsCollection;//안됨
    public Map map;//안됨
    public PortDatas allyPortDatas;//안됨
    public SaveData saveData;
    public MapData mapData;
    public RewardData rewardData;
}
