using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    float produceInterval = 0.1f;
    float produceTime = 15.0f;

    [SerializeField]
    PortDatas portDatas;
    GoodsCollection goodsCollection;
    [SerializeField]
    GameObject[] spawner;
    [SerializeField]
    Team team;
    string prefabPath;

    public UnityEvent turnHandler;
    public UnityEvent SetUI;

    Dictionary<string, GameObject> soldierPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        if(team == Team.Ally)
        {
            prefabPath = "SoldierPrefabs/Ally/";
            goodsCollection = SaveManager.Instance.gameData.goodsCollection;
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
        portDatas.round = 0;
        portDatas.cur_producetime = portDatas.produceTime;
        SetUI.Invoke();
    }

    public void battleStart()
    {
        GameManager.Instance.gamePause = false;
        Time.timeScale = 1;
        StartCoroutine(RoundControll());
    }

    void FixedUpdate()
    {
        //portDatas.spawnSoldierList.Sort((s1,s2) => ((HeroInfo)s2).healWeight.CompareTo(((HeroInfo)s1).healWeight));
    }

    IEnumerator RoundControll()
    {
        WaitUntil waitUntil = new WaitUntil(() => portDatas.cur_producetime <= 0);
        Coroutine timeManageCoroutine = StartCoroutine(ProduceTimeManage());
        yield return waitUntil;
        while (portDatas.round < portDatas.max_round)
        {
            portDatas.round++;
            portDatas.cur_producetime = produceTime;
            StartCoroutine(SpawnSoldier());
            turnHandler?.Invoke();
            if (goodsCollection != null)
            {
                goodsCollection.food += goodsCollection.foodIncome;//enemySpawner에도 goodsCollection 넣어주기?
            }
            SetUI.Invoke();
            yield return waitUntil;
        }
        StopCoroutine(timeManageCoroutine);
    }

    IEnumerator SpawnSoldier()
    {
        GameObject createSoldier;
        SoldierData soldierData;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                
                if (portDatas.portDatas[5 * i + j].soldierCode != null)
                {
                    soldierData = portDatas.activeSoldierList[portDatas.portDatas[5 * i + j].soldierCode];
                    createSoldier = Instantiate(soldierPrefabs[soldierData.code], spawner[j].transform.position, Quaternion.identity);
                    if (soldierData.mutant)
                    {
                        Instantiate(soldierData.mutant, createSoldier.transform);
                    }
                    if (soldierData.extraSkill != null)
                    {
                        for(int extraSkillIndex = 0; extraSkillIndex < soldierData.extraSkill.Count; extraSkillIndex++)
                        {
                            Instantiate(soldierData.extraSkill[extraSkillIndex], createSoldier.transform);
                        }
                    }
                    for (int unitAmount = 0; unitAmount < soldierData.unitAmount - 1; unitAmount++)
                    {
                        float temp = Random.Range(-1 - soldierData.size, 1 + soldierData.size);
                        Vector3 spawnPos = spawner[j].transform.position + new Vector3(temp, temp);
                        Instantiate(createSoldier, spawnPos, Quaternion.identity);
                    }
                }
            }
            yield return new WaitForSeconds(produceInterval);
        }
    }

    IEnumerator ProduceTimeManage()
    {
        while (true)
        {
            portDatas.cur_producetime -= Time.deltaTime;
            yield return null;
        }
    }
}
