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

    void BonusSupplyEffect()//3의 배수에서 스테이지 끝나면 흭득하고 추가 돈 받음
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