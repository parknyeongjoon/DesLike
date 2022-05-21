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
        //���� ���� �ҷ�����
        string filePath = Application.persistentDataPath + "GameData.json";
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);
            Debug.Log("�����ͺҷ�����");
        }
        else
        {
            gameData = new GameData();
            Debug.Log("������ ���� ����");
        }
        //���� ������ �ҷ�����
        LoadPortsDatas();
        LoadRelicData();
        LoadCurBattleNodeData();  //���� �߻�
        Debug.Log("�����ͺҷ�����Ϸ�");
    }

    public void SaveGameData()
    {
        //���� ������ ����
        SavePortDatas();
        SaveRelicData();
        SaveCurBattleNodeData(); //���� �߻�
        //���� �����͸� ���� ���Ϸ� ����
        string toJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "GameData.json";
        File.WriteAllText(filePath, toJsonData);
        Debug.Log("����������Ϸ�");
    }

    public void SaveNExit()
    {
        SaveGameData();
        Application.Quit();
    }

    public Map map;//�� ��ũ���ͺ� ������Ʈ
    public PortDatas allyPortDatas;//�Ʊ��� ����� PortDatas
    public GameObject heroPrefab;//���� ��� ���� ���� ������

    public void SavePortDatas()//��Ʈ �����͵��� �����ϴ� �Լ�
    {
        //�� ��Ʈ�� ���� ����
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            gameData.portSaveDatas.portSaveList[i].isUnlock = allyPortDatas.portDatas[i].unlock;
            gameData.portSaveDatas.portSaveList[i].soldierCode = allyPortDatas.portDatas[i].soldierCode;
            gameData.portSaveDatas.portSaveList[i].mutantCode = allyPortDatas.portDatas[i].mutantCode;
        }
        //��Ʈ ������ ����
        gameData.portSaveDatas.maxBarrierStrength = allyPortDatas.maxBarrierStrength;
        gameData.portSaveDatas.curBarrierStrength = allyPortDatas.curBarrierStrength;
    }

    public void LoadPortsDatas()//��Ʈ ������ �ҷ�����(Continue��ư������ ����� ��)
    {
        for (int i = 0; i < gameData.portSaveDatas.portSaveList.Length; i++)
        {
            if (gameData.portSaveDatas.portSaveList[i] == null) { gameData.portSaveDatas.portSaveList[i] = new PortSaveData(); }
            PortSaveData tempData = gameData.portSaveDatas.portSaveList[i];
            //SoldierData(Clone)�� ��Ƶδ� activeSoldierList�� �� �ҷ�����
            if (gameData.portSaveDatas.portSaveList[i].soldierCode != "" && !allyPortDatas.activeSoldierList.ContainsKey(tempData.soldierCode))//�ߺ��Ǵ� Ű�� ���ٸ� ����Ʈ�� �߰��ϱ�
            {
                allyPortDatas.activeSoldierList.Add(tempData.soldierCode, Instantiate(dataSheet.soldierDataSheet[tempData.soldierCode]));
            }
            //�� �Ҵ�
            allyPortDatas.portDatas[i].unlock = tempData.isUnlock;
            allyPortDatas.portDatas[i].soldierCode = tempData.soldierCode;
            allyPortDatas.portDatas[i].mutantCode = tempData.mutantCode;
        }
        //��Ʈ ������ ����
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

        for (int btn = 0; btn < 3; btn++)    // ��ư 1,2,3
        {
            selEvent[btn] = gameData.mapData.selEvent[btn];
            if (!(map.selectNode[btn] == null) && (selEvent[btn] == 0))  // �ʿ� ����Ǿ��ְ�, ���� ����� ���
            {
                if (gameData.mapData.curWindow == CurWindow.Battle)
                {
                    battleNode = (BattleNode)map.selectNode[btn];
                    for (int i = 0; i < battleNode.ableSoldierRewards.Count; i++)  // ableSoldier ����
                    {
                        InfiniteLoopDetector.Run();
                        gameData.curBattleNodeData.ableSoldierIndex[btn, i] = battleNode.ableSoldierRewards[i].code;
                    }
                    gameData.mapData.kingdom = battleNode.kingdom;

                    for (int i = 0; i < battleNode.reward.soldierReward.Count; i++)
                    {
                        InfiniteLoopDetector.Run();
                        gameData.curBattleNodeData.solRewardIndex[btn, i] = battleNode.reward.soldierReward[i].soldier.code;
                        // mutant �߰� �ʿ�
                    }
                }
                // ���� ���� �ʿ�
            }
        }
    }
    
    public void LoadCurBattleNodeData()
    {
        int[] selEvent = new int[3];
        for (int btn = 0; btn < 3; btn++)    // ��ư 1,2,3
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

                    for (int i = 0; i < battleNode.ableSoldierRewards.Count; i++)  // ableSoldier �ҷ�����
                    {
                        InfiniteLoopDetector.Run();
                        battleNode.ableSoldierRewards.Add(dataSheet.soldierDataSheet[gameData.curBattleNodeData.ableSoldierIndex[btn, i]]);
                    }

                    for (int i = 0; i < battleNode.reward.soldierReward.Count; i++)
                    {
                        InfiniteLoopDetector.Run();
                        soldierReward.soldier = dataSheet.soldierDataSheet[gameData.curBattleNodeData.solRewardIndex[btn, i]];
                        // mutant �߰� �ʿ�
                        battleNode.reward.soldierReward.Add(soldierReward);
                    }
                }
            }
        }
    }
}
