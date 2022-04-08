using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    PortDatas portDatas;
    [SerializeField]
    GameObject[] spawner;
    [SerializeField]
    Team team;

    string prefabPath;
    Dictionary<string, GameObject> soldierPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        if(team == Team.Ally)
        {
            prefabPath = "SoldierPrefabs/Ally/";
        }
        else if(team == Team.Enemy)
        {
            prefabPath = "SoldierPrefabs/Enemy/";
        }
        foreach(SoldierData soldierData in portDatas.activeSoldierList.Values)
        {
            soldierPrefabs.Add(soldierData.code, Resources.Load<GameObject>(prefabPath + soldierData.soldier_name));
        }
    }

    void Start()
    {
        portDatas.spawnSoldierList.Clear();
        SpawnSoldier();
        StartCoroutine(SortSoldierList());
    }

    void SpawnSoldier()
    {
        GameObject createSoldier;
        SoldierData soldierData;
        //영웅 소환
        if(team == Team.Ally && SaveManager.Instance.heroPrefab != null)
        {
            Instantiate(SaveManager.Instance.heroPrefab, new Vector3(-20, 0, 0), Quaternion.identity);
        }
        //병사 소환
        for (int portIndex = 0; portIndex < portDatas.portDatas.Length; portIndex++)
        {
            if (portDatas.portDatas[portIndex].soldierCode != "")
            {
                soldierData = portDatas.activeSoldierList[portDatas.portDatas[portIndex].soldierCode];
                createSoldier = Instantiate(soldierPrefabs[soldierData.code], spawner[portIndex].transform);
                if (portDatas.portDatas[portIndex].mutantCode != "")
                {
                    GameObject tempMutant = SaveManager.Instance.dataSheet.mutantObjectSheet[portDatas.portDatas[portIndex].mutantCode];
                    Instantiate(tempMutant, createSoldier.transform);
                }
                if (soldierData.extraSkill != null)
                {
                    for (int extraSkillIndex = 0; extraSkillIndex < soldierData.extraSkill.Count; extraSkillIndex++)
                    {
                        Instantiate(soldierData.extraSkill[extraSkillIndex], createSoldier.transform);
                    }
                }
                for (int unitAmount = 0; unitAmount < soldierData.unitAmount - 1; unitAmount++)
                {
                    float temp = Random.Range(-1 - soldierData.size, 1 + soldierData.size);
                    Vector3 spawnPos = spawner[portIndex].transform.position + new Vector3(temp, temp);
                    Instantiate(createSoldier, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    IEnumerator SortSoldierList()
    {
        while (true)
        {
            portDatas.spawnSoldierList.Sort((s1, s2) => ((HeroInfo)s2).healWeight.CompareTo(((HeroInfo)s1).healWeight));
            yield return null;
        }
    }
}
