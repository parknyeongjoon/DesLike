using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    Dictionary<string, SoldierData> activeSoldierList;
    int level;
    SaveManager saveManager;


    void OnEnable()
    {
        SaveManager saveManager = SaveManager.Instance;
        activeSoldierList = saveManager.allyPortDatas.activeSoldierList;
        level = saveManager.map.level;
    }

    public void InfectionOption1()//���� ���� ���� ����Ʈ
    {
        SaveManager.Instance.gameData.goodsSaveData.gold -= 100 * level;
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

    public void EndButton() // 2�� �Ҹ�
    {
        saveManager.gameData.mapData.curDay += 2;
    }
}
