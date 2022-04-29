using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBtnScript : MonoBehaviour
{
    SaveManager saveManager;

    [SerializeField]
    HeroData heroData;
    [SerializeField]
    PortsOption heroSelectSoldierOption;
    [SerializeField]
    GoodsCollection heroSelectGoodsCollection;
    [SerializeField]
    PortDatas allyPorts;
    [SerializeField]
    HeroDetailScript heroDetailScript;
    [SerializeField]
    GameObject heroPrefab;
    [SerializeField]
    GameObject campRelic;
    [SerializeField]
    MainTitle mainTitleScript;

    void Awake()
    {
        saveManager = SaveManager.Instance;
    }

    public void SelectHero()
    {
        mainTitleScript.startHandler = SetHeroPrefab;
        mainTitleScript.startHandler += SetHeroSaveData;
        mainTitleScript.startHandler += Set_PortsOption;
        mainTitleScript.startHandler += Set_GoodsCollection;
        mainTitleScript.startHandler += SetCampRelic;
    }

    void SetHeroPrefab()
    {
        SaveManager.Instance.heroPrefab = heroPrefab;
    }

    void SetHeroSaveData()
    {
        saveManager.gameData.heroSaveData.heroCode = heroData.code;
        saveManager.gameData.heroSaveData.cur_Hp = heroData.hp;
        saveManager.gameData.heroSaveData.cur_Mp = heroData.mp;
        saveManager.gameData.heroSaveData.resurrection = heroPrefab.GetComponent<HeroInfo>().resurrection;
    }

    void Set_PortsOption()
    {
        List<Option> option = heroSelectSoldierOption.soldierOption;
        for(int i = 0; i < allyPorts.portDatas.Length; i++)
        {
            allyPorts.portDatas[i].soldierCode = "";
            allyPorts.portDatas[i].mutantCode = "";
            allyPorts.portDatas[i].unlock = false;
        }
        allyPorts.activeSoldierList.Clear();
        for(int i = 0; i < 10; i++)//수 변경
        {
            allyPorts.portDatas[i].unlock = true;
        }
        for (int i = 0; i < option.Count; i++)
        {
            allyPorts.activeSoldierList.Add(option[i].soldierData.code, Instantiate(option[i].soldierData));
            for (int j = 0; j < option[i].portNum.Length; j++)
            {
                allyPorts.portDatas[option[i].portNum[j]].soldierCode = option[i].soldierData.code;
            }
        }
    }

   void Set_GoodsCollection()
    {
        SaveManager.Instance.gameData.goodsSaveData.gold = heroSelectGoodsCollection.goodsSaveData.gold;
        SaveManager.Instance.gameData.goodsSaveData.areaGold = heroSelectGoodsCollection.goodsSaveData.areaGold;
    }

    void SetCampRelic()
    {
        if (campRelic)
        {
            RelicManager relicManager;
            relicManager = RelicManager.Instance;
            Relic relicScript;
            relicScript = campRelic.GetComponent<Relic>();
            relicManager.relicList.Add(relicScript);
            Instantiate(campRelic, relicManager.relicCanvas.transform.GetChild(0).transform);
        }
    }

    public void Set_HeroDetail_Panel()
    {
        heroDetailScript.heroDetailPanel.SetActive(true);
        heroDetailScript.Hero_Img.sprite = heroData.sprite;
        GameManager.DeleteChilds(heroDetailScript.optionPreviewPanel);
        for(int i = 0; i < heroSelectSoldierOption.soldierOption.Count; i++)
        {
            heroDetailScript.CreateOptionPreview(heroSelectSoldierOption.soldierOption[i]);
        }
    }
}
