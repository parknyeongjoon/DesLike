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
        SaveManager.Instance.gameData = SaveManager.Instance.GameData;
        SceneManager.LoadScene("MainTitle");
    }

    public SoldierData[] soldierDatas;
    public HeroData[] heroDatas;
    public SkillData[] skillDatas;
    public GameObject[] mutantObjects;
    public MutantData[] mutantDatas;
    public GameObject[] relicObjects;
    public RelicData[] relicDatas;

    IEnumerator SetDataSheet()//Object�鵵 �߰�
    {
        for (int i = 0; i < soldierDatas.Length; i++)
        {
            if (!SaveManager.Instance.dataSheet.soldierDataSheet.ContainsKey(soldierDatas[i].code))//��ųʸ��� �߰��Ǿ����� �ʴٸ� �߰��ϱ�
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
        Debug.Log("���ÿϷ�");
        Debug.Log(SaveManager.Instance.dataSheet.soldierDataSheet.Count);
        yield return null;
    }
}
