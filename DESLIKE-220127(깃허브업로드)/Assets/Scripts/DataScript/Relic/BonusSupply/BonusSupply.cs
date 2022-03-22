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

    void BonusSupplyEffect()//3�� ������� �������� ������ ŉ���ϰ� �߰� �� ����
    {
        if(CheckCondition())
        {
            goodsCollection.gold += 200;
            Debug.Log("�߰����� ����");
        }
    }

    public bool CheckCondition()
    {
        if(isUse == false) { isUse = true; return true; }
        else { return false; }
    }
}