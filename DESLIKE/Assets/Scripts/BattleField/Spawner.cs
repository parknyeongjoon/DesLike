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

        for (int portIndex = 0; portIndex < portDatas.portDatas.Length; portIndex++)//portDatas.length/5
        {
            if (portDatas.portDatas[portIndex].soldierCode != null)
            {
                soldierData = portDatas.activeSoldierList[portDatas.portDatas[portIndex].soldierCode];
                createSoldier = Instantiate(soldierPrefabs[soldierData.code], spawner[portIndex].transform);
                if (soldierData.mutantCode != "0")
                {
                    // Instantiate(soldierData.mutantCode, createSoldier.transform);
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
