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
        LoadCurBattleNodeData();  //오류 발생
        Debug.Log("데이터불러오기완료");
    }

    public void SaveGameData()
    {
        //게임 데이터 저장
        SavePortDatas();
        SaveRelicData();
        SaveCurBattleNodeData(); //오류 발생
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

    public Map map;//맵 스크립터블 오브젝트
    public PortDatas allyPortDatas;//아군이 사용할 PortDatas
    public GameObject heroPrefab;//현재 사용 중인 영웅 프리팹

    public void SavePortDatas()//포트 데이터들을 저장하는 함수
    {
        //각 포트의 값들 저장
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            gameData.portSaveDatas.portSaveList[i].isUnlock = allyPortDatas.portDatas[i].unlock;
            gameData.portSaveDatas.portSaveList[i].soldierCode = allyPortDatas.portDatas[i].soldierCode;
            gameData.portSaveDatas.portSaveList[i].mutantCode = allyPortDatas.portDatas[i].mutantCode;
        }
        //포트 에너지 저장
        gameData.portSaveDatas.maxBarrierStrength = allyPortDatas.maxBarrierStrength;
        gameData.portSaveDatas.curBarrierStrength = allyPortDatas.curBarrierStrength;
    }

    public void LoadPortsDatas()//포트 데이터 불러오기(Continue버튼에서만 사용할 지)
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
        }
        //포트 에너지 저장
        allyPortDatas.maxBarrierStrength = gameData.portSaveDatas.maxBarrierStrength;
        allyPortDatas.curBarrierStrength = gameData.portSaveDatas.curBarrierStrength;
    }

    public void SaveHeroData(HeroInfo heroInfo)
    {
        gameData.heroSaveData.cur_Hp = heroInfo.cur_Hp;
        gameData.heroSaveData.cur_Mp = heroInfo.cur_Mp;
        gameData.heroSaveData.resurrection = heroInfo.resurrection;
    }

    public void SaveRelicData()
    {
        gameData.relicSaveData.Clear();
        foreach (var relic in RelicManager.instance.relicList.Values)
        {
            gameData.relicSaveData.Add(relic.relicData.code);
        }
    }

    public void LoadRelicData()
    {
        RelicManager.Instance.relicList.Clear();
        if(gameData.relicSaveData == null) { gameData.relicSaveData = new List<string>(); }
        for(int i = 0; i < gameData.relicSaveData.Count; i++)
        {
            RelicManager.instance.LoadRelic(gameData.relicSaveData[i]);
        }
    }

    BattleNode battleNode; 
    
    public void SaveCurBattleNodeData()   
    {
        int[] selEvent = new int[3];

        for (int btn = 0; btn < 3; btn++)    // 버튼 1,2,3
        {
            selEvent[btn] = gameData.mapData.selEvent[btn];
            if (!(map.selectNode[btn] == null) && (selEvent[btn] == 0))  // 맵에 저장되어있고, 전투 노드일 경우
            {
                if (gameData.mapData.curWindow == CurWindow.Battle)
                {
                    battleNode = (BattleNode)map.selectNode[btn];
                    for (int i = 0; i < battleNode.ableSoldierRewards.Count; i++)  // ableSoldier 저장
                    {
                        InfiniteLoopDetector.Run();
                        gameData.curBattleNodeData.ableSoldierIndex[btn, i] = battleNode.ableSoldierRewards[i].code;
                    }
                    gameData.mapData.kingdom = battleNode.kingdom;

                    for (int i = 0; i < battleNode.reward.soldierReward.Count; i++)
                    {
                        InfiniteLoopDetector.Run();
                        gameData.curBattleNodeData.solRewardIndex[btn, i] = battleNode.reward.soldierReward[i].soldier.code;
                        // mutant 추가 필요
                    }
                }
                // 유물 저장 필요
            }
        }
    }
    
    public void LoadCurBattleNodeData()
    {
        int[] selEvent = new int[3];
        for (int btn = 0; btn < 3; btn++)    // 버튼 1,2,3
        {
            selEvent[btn] = gameData.mapData.selEvent[btn];
            if ((map.selectNode[btn] != null) && (selEvent[btn] == 0))
            {
                if (gameData.mapData.curWindow == CurWindow.Battle)
                {
                    battleNode = (BattleNode)map.selectNode[btn];

                    battleNode.ableSoldierRewards.Clear();
                    // battleNode.ableRelicRewards.Clear();
                    battleNode.reward.soldierReward.Clear();
                    battleNode.kingdom = gameData.mapData.kingdom;

                    SoldierReward soldierReward = new SoldierReward();
                    SoldierData soldierData = new SoldierData();

                    for (int i = 0; i < battleNode.ableSoldierRewards.Count; i++)  // ableSoldier 불러오기
                    {
                        InfiniteLoopDetector.Run();
                        battleNode.ableSoldierRewards.Add(dataSheet.soldierDataSheet[gameData.curBattleNodeData.ableSoldierIndex[btn, i]]);
                    }

                    for (int i = 0; i < battleNode.reward.soldierReward.Count; i++)
                    {
                        InfiniteLoopDetector.Run();
                        soldierReward.soldier = dataSheet.soldierDataSheet[gameData.curBattleNodeData.solRewardIndex[btn, i]];
                        // mutant 추가 필요
                        battleNode.reward.soldierReward.Add(soldierReward);
                    }
                }
            }
        }
    }
}
