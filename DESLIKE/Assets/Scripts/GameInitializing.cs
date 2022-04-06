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

    void Start()
    {
        SetDataSheet();
        SceneManager.LoadScene("MainTitle");
    }

    public SoldierData[] soldierDatas;
    public HeroData[] heroDatas;
    public SkillData[] skillDatas;
    public GameObject[] mutantObjects;
    public MutantData[] mutantDatas;
    public GameObject[] relicObjects;
    public RelicData[] relicDatas;

    void SetDataSheet()
    {
        for (int i = 0; i < soldierDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.soldierDataSheet.ContainsKey(soldierDatas[i].code))//딕셔너리에 추가되어있지 않다면 추가하기
            {
                SaveManager.Instance.dataSheet.soldierDataSheet.Add(soldierDatas[i].code, soldierDatas[i]);
            }
        }
        for (int i = 0; i < heroDatas.Length; i++)
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
        for (int i = 0; i < mutantDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.mutantDataSheet.ContainsKey(mutantDatas[i].code))
            {
                SaveManager.Instance.dataSheet.mutantDataSheet.Add(mutantDatas[i].code, mutantDatas[i]);
            }
        }
    }
}
