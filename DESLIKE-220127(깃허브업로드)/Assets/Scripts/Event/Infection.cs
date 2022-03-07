using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    Dictionary<string, SoldierData> activeSoldierList;
    GoodsCollection goodsCollection;
    int level;

    void OnEnable()
    {
        activeSoldierList = SaveManager.Instance.activeSoldierList;
        goodsCollection = SaveManager.Instance.gameData.goodsCollection;
        level = SaveManager.Instance.gameData.map.level;
    }

    public void InfectionOption1()//·£´ý À¯´Ö ·£´ý ¹ÂÅÏÆ®
    {
        goodsCollection.gold -= 100 * level;
        int soldierIndex = Random.Range(0, activeSoldierList.Count);
        //int mutantIndex = Random.Range(0, 0);

    }

    public void InfectionOption2()
    {

    }

    public void InfectionOption3()
    {

    }

    public void InfectionOption4()
    {

    }

    public void EndButton() // 2ÀÏ ¼Ò¸ð
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 2;
    }
}
