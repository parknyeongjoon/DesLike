using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    static GameObject container;
    static SaveManager instance;

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "SaveManager";
                DontDestroyOnLoad(container);
                instance = container.AddComponent<SaveManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public DataSheet dataSheet;

    public GameData gameData;
    public GameData GameData
    {
        get
        {
            LoadGameData();
            SaveGameData();
            return gameData;
        }
    }

    public void LoadGameData()
    {
        //로컬 파일 불러오기
        string filePath = Application.persistentDataPath + "GameData.json";
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);
            Debug.Log("데이터불러오기");
        }
        else
        {
            gameData = new GameData();
            Debug.Log("데이터 새로 생성");
        }
        //게임 데이터 불러오기
        LoadPortsDatas();
        LoadRelicData();
        Debug.Log("데이터불러오기완료");
    }

    public void SaveGameData()
    {
        //게임 데이터 저장
        SavePortDatas();
        SaveRelicData();
        //게임 데이터를 로컬 파일로 저장
        string toJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "GameData.json";
        File.WriteAllText(filePath, toJsonData);
        Debug.Log("데이터저장완료");
    }

    public void SaveNExit()
    {
        SaveGameData();
        Application.Quit();
    }

    public PortDatas allyPortDatas;//아군이 사용할 PortDatas
    public Map map;//맵 스크립터블 오브젝트
    public GameObject heroPrefab;

    public void SavePortDatas()
    {
        //각 포트의 값들 저장
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            gameData.portSaveDatas.portSaveList[i].isUnlock = allyPortDatas.portDatas[i].unlock;
            gameData.portSaveDatas.portSaveList[i].soldierCode = allyPortDatas.portDatas[i].soldierCode;
            gameData.portSaveDatas.portSaveList[i].mutantCode = allyPortDatas.portDatas[i].mutantCode;
            Debug.Log("Save : " + gameData.portSaveDatas.portSaveList[i].soldierCode);
        }
        //포트 에너지 저장
        gameData.portSaveDatas.maxBarrierStrength = allyPortDatas.maxBarrierStrength;
        gameData.portSaveDatas.curBarrierStrength = allyPortDatas.curBarrierStrength;
    }

    public void LoadPortsDatas()//Continue버튼에서만 사용할 지
    {
        for (int i = 0; i < gameData.portSaveDatas.portSaveList.Length; i++)
        {
            if (gameData.portSaveDatas.portSaveList[i] == null) { gameData.portSaveDatas.portSaveList[i] = new PortSaveData(); }
            PortSaveData tempData = gameData.portSaveDatas.portSaveList[i];
            //SoldierData(Clone)을 담아두는 activeSoldierList에 값 불러오기
            if (gameData.portSaveDatas.portSaveList[i].soldierCode != "" && !allyPortDatas.activeSoldierList.ContainsKey(tempData.soldierCode))//중복되는 키가 없다면 리스트에 추가하기
            {
                allyPortDatas.activeSoldierList.Add(tempData.soldierCode, Instantiate(dataSheet.soldierDataSheet[tempData.soldierCode]));
            }
            //값 할당
            allyPortDatas.portDatas[i].unlock = tempData.isUnlock;
            allyPortDatas.portDatas[i].soldierCode = tempData.soldierCode;
            allyPortDatas.portDatas[i].mutantCode = tempData.mutantCode;
            Debug.Log("Load" + allyPortDatas.portDatas[i].soldierCode);
        }
        //포트 에너지 저장
        allyPortDatas.maxBarrierStrength = gameData.portSaveDatas.maxBarrierStrength;
        allyPortDatas.curBarrierStrength = gameData.portSaveDatas.curBarrierStrength;
    }

    public void SaveRelicData()
    {
        gameData.relicSaveData.Clear();
        for(int i = 0; i < RelicManager.Instance.relicList.Count; i++)
        {
            gameData.relicSaveData.Add(RelicManager.Instance.relicList[i].relicData.code);
        }
    }

    public void LoadRelicData()
    {
        RelicManager.Instance.relicList.Clear();
        for(int i = 0; i < gameData.relicSaveData.Count; i++)
        {
            RelicManager.Instance.relicList.Add(dataSheet.relicObjectSheet[gameData.relicSaveData[i]].GetComponent<Relic>());
        }
    }

    MapData mapSaveData;

    public void SaveMapSaveData()   // 이거 맞아요?
    {
        gameData.mapData.kingdom = mapSaveData.kingdom;
        gameData.mapData.curWindow = mapSaveData.curWindow;
        gameData.mapData.curStage = mapSaveData.curStage;
        gameData.mapData.curTrack = mapSaveData.curTrack;
        gameData.mapData.curDay = mapSaveData.curDay;  // 현재 날짜
        gameData.mapData.curBtn = mapSaveData.curBtn;  // 현재 어떤 버튼을 눌러서 이 곳으로 왔는지

        gameData.mapData.challengeCount = mapSaveData.challengeCount;
        gameData.mapData.midBossCheck1 = mapSaveData.midBossCheck1;
        gameData.mapData.midBossCheck2 = mapSaveData.midBossCheck2;
        gameData.mapData.villageCheck = mapSaveData.villageCheck;
        gameData.mapData.organCheck = mapSaveData.organCheck;
        gameData.mapData.newSet = mapSaveData.newSet; // 중간 보스, 마을, 정비, 이미 세팅했는지 여부
        for (int i = 0; i < 3; i++)
        {
            gameData.mapData.selEvent[i] = mapSaveData.selEvent[i];
            gameData.mapData.nextEvent[i] = mapSaveData.nextEvent[i];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
            gameData.mapData.evntList[i] = mapSaveData.evntList[i];    // 이벤트가 어떤 이벤트인지 저장, 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
            gameData.mapData.isEventSet[i] = mapSaveData.isEventSet[i];
            gameData.mapData.isRewardSet[i] = mapSaveData.isRewardSet[i];
            gameData.mapData.isAbleSet[i] = mapSaveData.isAbleSet[i];
            gameData.mapData.isChallenge[i] = mapSaveData.isChallenge[i];
        }
    }

    public void LoadMapSaveData()   // ???
    {
        mapSaveData.kingdom = gameData.mapData.kingdom;
        mapSaveData.curWindow = gameData.mapData.curWindow;
        mapSaveData.curStage = gameData.mapData.curStage;
        mapSaveData.curTrack = gameData.mapData.curTrack;
        mapSaveData.curDay = gameData.mapData.curDay;  // 현재 날짜
        mapSaveData.curBtn = gameData.mapData.curBtn;  // 현재 어떤 버튼을 눌러서 이 곳으로 왔는지

        mapSaveData.challengeCount = gameData.mapData.challengeCount;
        mapSaveData.midBossCheck1 = gameData.mapData.midBossCheck1;
        mapSaveData.midBossCheck2 = gameData.mapData.midBossCheck2;
        mapSaveData.villageCheck = gameData.mapData.villageCheck;
        mapSaveData.organCheck = gameData.mapData.organCheck;
        mapSaveData.newSet = gameData.mapData.newSet; // 중간 보스, 마을, 정비, 이미 세팅했는지 여부
        for (int i = 0; i < 3; i++)
        {
            mapSaveData.selEvent[i] = gameData.mapData.selEvent[i];
            mapSaveData.nextEvent[i] = gameData.mapData.nextEvent[i];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
            mapSaveData.evntList[i] = gameData.mapData.evntList[i];    // 이벤트가 어떤 이벤트인지 저장, 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
            mapSaveData.isEventSet[i] = gameData.mapData.isEventSet[i];
            mapSaveData.isRewardSet[i] = gameData.mapData.isRewardSet[i];
            mapSaveData.isAbleSet[i] = gameData.mapData.isAbleSet[i];
            mapSaveData.isChallenge[i] = gameData.mapData.isChallenge[i];
        }
    }
}
