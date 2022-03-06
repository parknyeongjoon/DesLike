using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainTitle : MonoBehaviour
{
    [SerializeField]
    GameObject Dictionary_Panel, HeroSelect_Panel;
    [SerializeField]
    Map map;//여러 개 넣어놓고 랜덤으로 하는 방법도 좋을 듯
    [SerializeField]
    PortDatas allyPortDatas;
    [SerializeField]
    GameObject hero;
    [SerializeField]
    HeroInfo heroInfo;
    [SerializeField]
    Button continueBtn;

    public delegate void StartHandler();
    public StartHandler startHandler;

    SaveManager saveManager;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        if (!saveManager.gameData.canContinue)
        {
            continueBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            continueBtn.interactable = false;
        }
        else
        {
            continueBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            continueBtn.interactable = true;
        }
    }

    public void NewGameStart()
    {
        map.isMap = false;
        saveManager.gameData.map = map;
        saveManager.gameData.allyPortDatas = allyPortDatas;
        startHandler?.Invoke();
        hero.SetActive(true);
        SceneManager.LoadScene("StageSelect");
    }

    public void Continue()
    {
        heroInfo.castleData = saveManager.gameData.heroSaveData.heroData;
        SaveManager.Instance.LoadSoldierData();
        hero.SetActive(true);
        SceneManager.LoadScene("Map");
    }

    public void Open_Dic()
    {
        Dictionary_Panel.SetActive(true);
    }

    public void Close_Dic()
    {
        Dictionary_Panel.SetActive(false);
    }

    public void Open_Hero_Select()
    {
        HeroSelect_Panel.SetActive(true);
    }

    public void Close_Hero_Select()
    {
        HeroSelect_Panel.SetActive(false);
    }

    public void Exit()
    {
        saveManager.SaveNExit();
    }

    public SoldierData[] soldierDatas;
    public HeroData[] heroDatas;
    public SkillData[] skillDatas;

    public void SaveDataSheet()//지우기
    {
        SaveManager.Instance.dataSheet.soldierDataSheet.Clear();
        SaveManager.Instance.dataSheet.heroDataSheet.Clear();
        SaveManager.Instance.dataSheet.skillDataSheet.Clear();
        for(int i = 0; i < soldierDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.soldierDataSheet.ContainsKey(soldierDatas[i].code))//딕셔너리에 추가되어있지 않다면 추가하기
            {
                SaveManager.Instance.dataSheet.soldierDataSheet.Add(soldierDatas[i].code, soldierDatas[i]);
            }
        }
        for(int i = 0; i < heroDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.heroDataSheet.ContainsKey(heroDatas[i].code))
            {
                SaveManager.Instance.dataSheet.heroDataSheet.Add(heroDatas[i].code, heroDatas[i]);
            }
        }
        for (int i = 0; i < skillDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.skillDataSheet.ContainsKey(skillDatas[i].code))
            {
                SaveManager.Instance.dataSheet.skillDataSheet.Add(skillDatas[i].code, skillDatas[i]);
            }
        }
        SaveManager.Instance.SaveDataSheet();
    }
}
