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
    Button continueBtn;

    public delegate void StartHandler();
    public StartHandler startHandler;

    SaveManager saveManager;

    void Awake()
    {
        GameManager.Instance.GamePause(true);
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
        string prefabPath = "HeroPrefabs/" + SaveManager.Instance.dataSheet.heroDataSheet[SaveManager.Instance.gameData.heroSaveData.heroCode].soldier_name;
        SaveManager.Instance.heroPrefab = Resources.Load<GameObject>(prefabPath);
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

    public void Test()//지우기
    {
        SceneManager.LoadScene("Test");
    }


}
