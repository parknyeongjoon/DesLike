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

    BattleNode battleNode; 
    
    public void SaveCurBattleNodeData()   
    {
        for(int btn = 0; btn<3; btn++)    // 버튼 1,2,3
        {
            battleNode = (BattleNode)map.selectNode[btn];
            for(int i = 0; i<battleNode.ableSoldierRewards.Count; i++)  // ableSoldier 저장
                gameData.curBattleNodeData.ableSoldierIndex[btn,i] = battleNode.ableSoldierRewards[i].code;
            gameData.mapData.kingdom = battleNode.kingdom;

            for(int i = 0; i<battleNode.reward.soldierReward.Count; i++)
            {
                gameData.curBattleNodeData.solRewardIndex[btn, i] = battleNode.reward.soldierReward[i].soldier.code;
                // mutant 추가 필요
            }
            
            // 유물 저장 필요
        }
    }
    
    public void LoadCurBattleNodeData()
    {
        for (int btn = 0; btn < 3; btn++)    // 버튼 1,2,3
        {
            battleNode = (BattleNode)map.selectNode[btn];

            battleNode.ableSoldierRewards.Clear();
            // battleNode.ableRelicRewards.Clear();
            battleNode.reward.soldierReward.Clear();
            battleNode.kingdom = gameData.mapData.kingdom;

            SoldierReward soldierReward = new SoldierReward();
            SoldierData soldierData = new SoldierData();

            for (int i = 0; i < battleNode.ableSoldierRewards.Count; i++)  // ableSoldier 불러오기
                battleNode.ableSoldierRewards.Add(dataSheet.soldierDataSheet[gameData.curBattleNodeData.ableSoldierIndex[btn, i]]);


            for (int i = 0; i < battleNode.reward.soldierReward.Count; i++)
            {
                soldierReward.soldier = dataSheet.soldierDataSheet[gameData.curBattleNodeData.solRewardIndex[btn, i]];
                // mutant 추가 필요

                battleNode.reward.soldierReward.Add(soldierReward);
            }
        }
    }
}
