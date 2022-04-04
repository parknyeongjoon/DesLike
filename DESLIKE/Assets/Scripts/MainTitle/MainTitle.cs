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
            continueBtn.interactable = false;
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

    public void BaseCamp()
    {
        SceneManager.LoadScene("BaseCamp");
    }

    public void Test()
    {
        SceneManager.LoadScene("Test");
    }
}
