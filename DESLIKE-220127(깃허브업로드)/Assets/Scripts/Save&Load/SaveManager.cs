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
            if (!dataSheet.soldierDataSheet.ContainsKey(soldierDatas[i].code))//딕셔너리에 추가되어있지 않다면 추가하기
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
        Debug.Log("데이터불러오기완료");
    }

    public void SaveGameData()
    {
        string toJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "GameData.json";
        File.WriteAllText(filePath, toJsonData);
        //SaveSoldierData();
        Debug.Log("데이터저장완료");
    }

    public void SaveNExit()
    {
        SaveGameData();
        Application.Quit();
    }

    public Dictionary<string, SoldierData> activeSoldierList = new Dictionary<string, SoldierData>();

    public void SaveSoldierData()
    {
        gameData.soldierSaveList.Clear();//키 중복 체크하기
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
        activeSoldierList.Clear();//키중복 체크하기
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
