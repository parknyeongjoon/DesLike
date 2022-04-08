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
    HeroInfo heroInfo;
    [SerializeField]
    Button continueBtn;

    public delegate void StartHandler();
    public StartHandler startHandler;

    SaveManager saveManager;

    void Awake()
    {
        Time.timeScale = 0;
        GameManager.Instance.gamePause = true;
        saveManager = SaveManager.Instance;
        if (!saveManager.gameData.canContinue)
        {
            continueBtn.interactable = false;
        }
    }

    public void NewGameStart()
    {
        saveManager.map.isMap = false;
        saveManager.gameData.canContinue = true;
        startHandler?.Invoke();
        SceneManager.LoadScene("StageSelect");
    }

    public void Continue()
    {
        heroInfo.castleData = saveManager.gameData.heroSaveData.heroData;
        //SaveMangaer.gameData에서 정보 불러오기
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
