using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        gameData = GameData;
        dataSheet = DataSheet;
    }

    public DataSheet dataSheet;
    public DataSheet DataSheet
    {
        get
        {
            if(dataSheet == null)
            {
                LoadDataSheet();
            }
            return dataSheet;
        }
    }

    public SoldierData[] soldierDatas;
    public HeroData[] heroDatas;
    public SkillData[] skillDatas;

    public void LoadDataSheet()
    {
        dataSheet.soldierDataSheet.Clear();
        dataSheet.heroDataSheet.Clear();
        dataSheet.skillDataSheet.Clear();
        for (int i = 0; i < soldierDatas.Length; i++)
        {
            if (!dataSheet.soldierDataSheet.ContainsKey(soldierDatas[i].code))//��ųʸ��� �߰��Ǿ����� �ʴٸ� �߰��ϱ�
            {
                dataSheet.soldierDataSheet.Add(soldierDatas[i].code, soldierDatas[i]);
            }
        }
        for (int i = 0; i < heroDatas.Length; i++)
        {
            if (!dataSheet.heroDataSheet.ContainsKey(heroDatas[i].code))
            {
                dataSheet.heroDataSheet.Add(heroDatas[i].code, heroDatas[i]);
            }
        }
        for (int i = 0; i < skillDatas.Length; i++)
        {
            if (!dataSheet.skillDataSheet.ContainsKey(skillDatas[i].code))
            {
                dataSheet.skillDataSheet.Add(skillDatas[i].code, skillDatas[i]);
            }
        }
    }

    public GameData gameData;
    public GameData GameData
    {
        get
        {
            if (gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "GameData.json";
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);
        }
        else
        {
            gameData = new GameData();
        }
        //LoadSoldierData();
        Debug.Log("�����ͺҷ�����Ϸ�");
    }

    public void SaveGameData()
    {
        string toJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "GameData.json";
        File.WriteAllText(filePath, toJsonData);
        //SaveSoldierData();
        Debug.Log("����������Ϸ�");
    }

    public void SaveNExit()
    {
        SaveGameData();
        Application.Quit();
    }

    public Dictionary<string, SoldierData> activeSoldierList = new Dictionary<string, SoldierData>();

    public void SaveSoldierData()
    {
        gameData.soldierSaveList.Clear();//Ű �ߺ� üũ�ϱ�
        if (activeSoldierList != null)
        {
            foreach (SoldierData soldierData in activeSoldierList.Values)
            {
                SoldierSaveData tempSoldierSaveData = new SoldierSaveData();
                tempSoldierSaveData.soldierCode = soldierData.code;
                tempSoldierSaveData.mutant = soldierData.mutant;
                tempSoldierSaveData.remain = soldierData.remain;
                gameData.soldierSaveList.Add(tempSoldierSaveData);
            }
        }
    }

    public void LoadSoldierData()
    {
        activeSoldierList.Clear();//Ű�ߺ� üũ�ϱ�
        for(int i = 0; i < gameData.soldierSaveList.Count; i++)
        {
            SoldierData tempSoldierData;
            tempSoldierData = Instantiate(dataSheet.soldierDataSheet[gameData.soldierSaveList[i].soldierCode]);
            tempSoldierData.mutant = gameData.soldierSaveList[i].mutant;
            tempSoldierData.remain = gameData.soldierSaveList[i].remain;
            activeSoldierList.Add(tempSoldierData.code, tempSoldierData);
        }
    }
}
