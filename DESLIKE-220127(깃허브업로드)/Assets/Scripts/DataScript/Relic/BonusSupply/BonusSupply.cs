using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusSupply : Relic
{
    PortDatas allyPortDatas;
    GoodsCollection goodsCollection;

    void Awake()
    {
        allyPortDatas = SaveManager.Instance.gameData.allyPortDatas;
        goodsCollection = SaveManager.Instance.gameData.goodsCollection;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)//�ű��?
    {
        if(scene.name == "Battle Field")
        {
            AddEffect();
        }
    }

    void AddEffect()
    {
        GameObject.Find("AllySpawner").GetComponent<Spawner>().turnHandler.AddListener(BonusSupplyEffect);
    }

    void BonusSupplyEffect()//3�� ������� �������� ������ ŉ���ϰ� �߰� �� ����
    {
        if(CheckCondition())
        {
            goodsCollection.food += goodsCollection.foodIncome / 2;
        }
    }

    public bool CheckCondition()
    {
        if(allyPortDatas.round != 0 && allyPortDatas.round % 3 == 0)
        {
            StartCoroutine(ConditionEffect());
            return true;
        }
        return false;
    }
}
