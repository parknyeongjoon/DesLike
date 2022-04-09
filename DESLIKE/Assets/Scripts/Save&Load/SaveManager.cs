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
        Debug.Log("�����ͺҷ�����Ϸ�");
    }

    public void SaveGameData()
    {
        //���� ������ ����
        SavePortDatas();
        SaveRelicData();
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

    public PortDatas allyPortDatas;//�Ʊ��� ����� PortDatas
    public Map map;//�� ��ũ���ͺ� ������Ʈ
    public GameObject heroPrefab;

    public void SavePortDatas()
    {
        //�� ��Ʈ�� ���� ����
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            gameData.portSaveDatas.portSaveList[i].isUnlock = allyPortDatas.portDatas[i].unlock;
            gameData.portSaveDatas.portSaveList[i].soldierCode = allyPortDatas.portDatas[i].soldierCode;
            gameData.portSaveDatas.portSaveList[i].mutantCode = allyPortDatas.portDatas[i].mutantCode;
            Debug.Log("Save : " + gameData.portSaveDatas.portSaveList[i].soldierCode);
        }
        //��Ʈ ������ ����
        gameData.portSaveDatas.maxBarrierStrength = allyPortDatas.maxBarrierStrength;
        gameData.portSaveDatas.curBarrierStrength = allyPortDatas.curBarrierStrength;
    }

    public void LoadPortsDatas()//Continue��ư������ ����� ��
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
            Debug.Log("Load" + allyPortDatas.portDatas[i].soldierCode);
        }
        //��Ʈ ������ ����
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

    public void SaveMapSaveData()   // �̰� �¾ƿ�?
    {
        gameData.mapData.kingdom = mapSaveData.kingdom;
        gameData.mapData.curWindow = mapSaveData.curWindow;
        gameData.mapData.curStage = mapSaveData.curStage;
        gameData.mapData.curTrack = mapSaveData.curTrack;
        gameData.mapData.curDay = mapSaveData.curDay;  // ���� ��¥
        gameData.mapData.curBtn = mapSaveData.curBtn;  // ���� � ��ư�� ������ �� ������ �Դ���

        gameData.mapData.challengeCount = mapSaveData.challengeCount;
        gameData.mapData.midBossCheck1 = mapSaveData.midBossCheck1;
        gameData.mapData.midBossCheck2 = mapSaveData.midBossCheck2;
        gameData.mapData.villageCheck = mapSaveData.villageCheck;
        gameData.mapData.organCheck = mapSaveData.organCheck;
        gameData.mapData.newSet = mapSaveData.newSet; // �߰� ����, ����, ����, �̹� �����ߴ��� ����
        for (int i = 0; i < 3; i++)
        {
            gameData.mapData.selEvent[i] = mapSaveData.selEvent[i];
            gameData.mapData.nextEvent[i] = mapSaveData.nextEvent[i];   // ���� �̺�Ʈ(����) �����. 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
            gameData.mapData.evntList[i] = mapSaveData.evntList[i];    // �̺�Ʈ�� � �̺�Ʈ���� ����, 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
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
        mapSaveData.curDay = gameData.mapData.curDay;  // ���� ��¥
        mapSaveData.curBtn = gameData.mapData.curBtn;  // ���� � ��ư�� ������ �� ������ �Դ���

        mapSaveData.challengeCount = gameData.mapData.challengeCount;
        mapSaveData.midBossCheck1 = gameData.mapData.midBossCheck1;
        mapSaveData.midBossCheck2 = gameData.mapData.midBossCheck2;
        mapSaveData.villageCheck = gameData.mapData.villageCheck;
        mapSaveData.organCheck = gameData.mapData.organCheck;
        mapSaveData.newSet = gameData.mapData.newSet; // �߰� ����, ����, ����, �̹� �����ߴ��� ����
        for (int i = 0; i < 3; i++)
        {
            mapSaveData.selEvent[i] = gameData.mapData.selEvent[i];
            mapSaveData.nextEvent[i] = gameData.mapData.nextEvent[i];   // ���� �̺�Ʈ(����) �����. 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
            mapSaveData.evntList[i] = gameData.mapData.evntList[i];    // �̺�Ʈ�� � �̺�Ʈ���� ����, 0 : 1Ʈ��, 1 : 2Ʈ��, 2 : 3Ʈ��
            mapSaveData.isEventSet[i] = gameData.mapData.isEventSet[i];
            mapSaveData.isRewardSet[i] = gameData.mapData.isRewardSet[i];
            mapSaveData.isAbleSet[i] = gameData.mapData.isAbleSet[i];
            mapSaveData.isChallenge[i] = gameData.mapData.isChallenge[i];
        }
    }
}
