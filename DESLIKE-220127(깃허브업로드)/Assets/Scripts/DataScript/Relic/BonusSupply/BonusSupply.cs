using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusSupply : Relic
{
    PortDatas allyPortDatas;
    GoodsCollection goodsCollection;

    bool isUse = false;

    void Awake()
    {
        goodsCollection = SaveManager.Instance.gameData.goodsCollection;
        BonusSupplyEffect();
    }

    void BonusSupplyEffect()//3의 배수에서 스테이지 끝나면 흭득하고 추가 돈 받음
    {
        if(CheckCondition())
        {
            goodsCollection.gold += 200;
            Debug.Log("추가보급 적용");
        }
    }

    public bool CheckCondition()
    {
        if(isUse == false) { isUse = true; return true; }
        else { return false; }
    }
}