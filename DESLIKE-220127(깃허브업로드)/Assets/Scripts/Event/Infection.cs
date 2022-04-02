using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    Dictionary<string, SoldierData> activeSoldierList;
    GoodsCollection goodsCollection;
    int level;
    SaveManager saveManager;


    void OnEnable()
    {
        SaveManager saveManager = SaveManager.Instance;
        activeSoldierList = saveManager.activeSoldierList;
        goodsCollection = saveManager.gameData.goodsCollection;
        level = saveManager.gameData.map.level;
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
        saveManager.gameData.mapData.curDay += 2;
    }
}
