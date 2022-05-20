using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializing : MonoBehaviour
{
    void Awake()
    {
        GameManager gameManager = GameManager.Instance;
    }

    IEnumerator Start()
    {
        yield return SetDataSheet();
        yield return divideMapRel();
        SaveManager.Instance.gameData = SaveManager.Instance.GameData;
        SceneManager.LoadScene("MainTitle");
    }

    public Map map;
    public SoldierData[] soldierDatas;
    public HeroData[] heroDatas;
    public SkillData[] skillDatas;
    public GameObject[] mutantObjects;
    public GameObject[] relicObjects;
    public BattleNodeScript BattleNodeScript; 
    
    IEnumerator SetDataSheet()//Object들도 추가
    {
        for (int i = 0; i < soldierDatas.Length; i++)
        {
            SaveManager.Instance.dataSheet.soldierDataSheet.Add(soldierDatas[i].code, soldierDatas[i]);
        }
        for (int i = 0; i < heroDatas.Length; i++)
        {
            SaveManager.Instance.dataSheet.heroDataSheet.Add(heroDatas[i].code, heroDatas[i]);
        }
        for (int i = 0; i < skillDatas.Length; i++)
        {
            SaveManager.Instance.dataSheet.skillDataSheet.Add(skillDatas[i].code, skillDatas[i]);
        }
        for (int i = 0; i < mutantObjects.Length; i++)
        {
            Mutant tempMutant = mutantObjects[i].GetComponent<Mutant>();
            SaveManager.Instance.dataSheet.mutantObjectSheet.Add(tempMutant.mutantData.code, mutantObjects[i]);
            SaveManager.Instance.dataSheet.mutantDataSheet.Add(tempMutant.mutantData.code, tempMutant.mutantData);
        }
        for(int i = 0; i < relicObjects.Length; i++)
        {
            Debug.Log(i);
            Relic tempRelic = relicObjects[i].GetComponent<Relic>();
            SaveManager.Instance.dataSheet.relicObjectSheet.Add(tempRelic.relicData.code, relicObjects[i]);
            SaveManager.Instance.dataSheet.relicDataSheet.Add(tempRelic.relicData.code, tempRelic.relicData);
        }
        Debug.Log("세팅완료");
        Debug.Log(SaveManager.Instance.dataSheet.soldierDataSheet.Count);
        yield return null;
    }

    IEnumerator divideMapRel()
    {
        /*
        map.commonNorRel.Clear();
        map.commonEpicRel.Clear();
        map.commonLegendRel.Clear();
        map.physicNorRel.Clear();
        map.physicEpicRel.Clear();
        map.physicLegendRel.Clear();
        map.spellNorRel.Clear();
        map.spellEpicRel.Clear();
        map.spellLegendRel.Clear();
        */
        for (int i = 0; i < relicObjects.Length; i++)
        {
            Relic tempRelic = relicObjects[i].GetComponent<Relic>();
            switch (tempRelic.relicData.kingdom)
            {
                case Kingdom.Common:
                    {
                        switch (tempRelic.relicData.rarity)
                        {
                            case Rarity.Normal:
                                map.commonNorRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Epic:
                                map.commonEpicRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Hero:
                                map.commonLegendRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                        }
                    }
                        break;
                case Kingdom.Physic:
                    {
                        switch (tempRelic.relicData.rarity)
                        {
                            case Rarity.Normal:
                                map.physicNorRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Epic:
                                map.physicEpicRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Hero:
                                map.physicLegendRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                        }
                    }
                    break;
                case Kingdom.Spell:
                    {
                        switch (tempRelic.relicData.rarity)
                        {
                            case Rarity.Normal:
                                map.spellNorRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Epic:
                                map.spellEpicRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                            case Rarity.Hero:
                                map.spellLegendRel.Add(tempRelic.relicData.code, tempRelic);
                                break;
                        }
                    }
                    break;
            }
        }
        yield return null;
    }
}