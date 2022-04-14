using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resurrection : Relic
{
    [SerializeField]
    GameObject resurrectionEffect;
    RelicManager relicManager;

    void Awake()
    {
        relicManager = RelicManager.Instance;
        relicManager.soldierConditionHandler += AddEffect;
        foreach(SoldierData soldierData in SaveManager.Instance.allyPortDatas.activeSoldierList.Values)
        {
            AddEffect(soldierData);
        }
    }

    void AddEffect(SoldierData soldierData)
    {
        if (CheckCondition(soldierData))
        {
            soldierData.extraSkill.Add(resurrectionEffect);
        }
    }

    public bool CheckCondition(SoldierData soldierData)
    {
        if (soldierData.tribe == Tribe.Rat)
        {
            StartCoroutine(ConditionEffect());
            return true;
        }
        return false;
    }
}
