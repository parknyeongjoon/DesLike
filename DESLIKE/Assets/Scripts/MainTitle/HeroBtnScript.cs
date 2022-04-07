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
    HeroInfo heroInfo;
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
        mainTitleScript.startHandler = SetHeroInfo;
        mainTitleScript.startHandler += SetHeroSaveData;
        mainTitleScript.startHandler += Set_PortsOption;
        mainTitleScript.startHandler += Set_GoodsCollection;
        mainTitleScript.startHandler += SetCampRelic;
    }

    void SetHeroInfo()
    {
        heroInfo.castleData = heroData;
    }

    void SetHeroSaveData()
    {
        saveManager.gameData.heroSaveData.heroData = heroData;
        saveManager.gameData.heroSaveData.cur_Hp = heroData.hp;
        saveManager.gameData.heroSaveData.cur_Mp = heroData.mp;
        saveManager.gameData.heroSaveData.resurrection = heroInfo.resurrection;
    }

    void Set_PortsOption()
    {
        List<Option> option = heroSelectSoldierOption.soldierOption;
        //빌드 전 초기화 해주고 지우기
        for(int i = 0; i < allyPorts.portDatas.Length; i++)
        {
            allyPorts.portDatas[i].soldierCode = null;
            allyPorts.portDatas[i].mutantCode = null;
        }
        //지우기
        allyPorts.activeSoldierList = new Dictionary<string, SoldierData>();
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
        SaveManager.Instance.gameData.goodsSaveData.magicalStone = heroSelectGoodsCollection.goodsSaveData.magicalStone;
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
