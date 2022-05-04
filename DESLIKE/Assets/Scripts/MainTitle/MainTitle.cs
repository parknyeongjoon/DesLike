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
        AkSoundEngine.PostEvent("Music_Start", gameObject);
    }

    public void NewGameStart()
    {
        saveManager.map.isMap = false;
        saveManager.gameData.canContinue = true;
        startHandler?.Invoke();
        RelicManager.Instance.relicList = new List<Relic>();
        SceneManager.LoadScene("Map");
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
    //이 밑으로 지우기
    [SerializeField] PortDatas allyPortDatas, enemyPortDatas;
    [SerializeField] PortsOption allyOption, enemyOption, sandbagOption;
    [SerializeField] GameObject heroPrefab;

    public void TestSH()
    {
        SaveManager.Instance.heroPrefab = heroPrefab;
        saveManager.gameData.heroSaveData.cur_Hp = 100;
        saveManager.gameData.heroSaveData.cur_Mp = 100;
        Set_PortsOption(allyOption, allyPortDatas);
        Set_PortsOption(enemyOption, enemyPortDatas);
        SceneManager.LoadScene("Battle Field");
    }

    public void TestJY()
    {
        SaveManager.Instance.heroPrefab = heroPrefab;
        saveManager.gameData.heroSaveData.cur_Hp = 100;
        saveManager.gameData.heroSaveData.cur_Mp = 100;
        Set_PortsOption(allyOption, allyPortDatas);
        Set_PortsOption(sandbagOption, enemyPortDatas);
        SceneManager.LoadScene("Battle Field");
    }

    void Set_PortsOption(PortsOption portsOption, PortDatas portDatas)
    {
        List<Option> option = portsOption.soldierOption;
        for (int i = 0; i < portDatas.portDatas.Length; i++)
        {
            portDatas.portDatas[i].soldierCode = "";
            portDatas.portDatas[i].mutantCode = "";
            portDatas.portDatas[i].unlock = false;
        }
        portDatas.activeSoldierList.Clear();
        for (int i = 0; i < option.Count; i++)
        {
            portDatas.activeSoldierList.Add(option[i].soldierData.code, Instantiate(option[i].soldierData));
            for (int j = 0; j < option[i].portNum.Length; j++)
            {
                portDatas.portDatas[option[i].portNum[j]].soldierCode = option[i].soldierData.code;
            }
        }
    }
}
