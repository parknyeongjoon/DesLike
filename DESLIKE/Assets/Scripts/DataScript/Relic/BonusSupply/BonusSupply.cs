using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusSupply : Relic
{
    PortDatas allyPortDatas;

    bool isUse = false;

    void Awake()
    {
        BonusSupplyEffect();
    }

    void BonusSupplyEffect()
    {
        if(CheckCondition())
        {
            SaveManager.Instance.gameData.goodsSaveData.gold += 200;
            Debug.Log("추가보급 적용");
        }
    }

    public bool CheckCondition()
    {
        if(isUse == false) { isUse = true; return true; }
        else { return false; }
    }
}