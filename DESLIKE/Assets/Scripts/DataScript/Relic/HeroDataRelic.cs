using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataRelic : Relic
{
    public override void DoEffect()
    {
        foreach (var heroData in SaveManager.Instance.allyPortDatas.activeSoldierList.Values)
        {
            Effect(heroData);
        }
        RelicManager.instance.soldierConditionCheck += Effect;
    }

    public void Effect(HeroData heroData)
    {
        if (((HeroDataRelicData)relicData).ConditionCheck(heroData))
        {
            ((HeroDataRelicData)relicData).Effect(heroData);
            StartCoroutine(ConditionEffect());
        }
    }
}
