using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRelic : Relic
{
    public override void DoEffect()
    {
        Effect();
    }

    public override void RemoveEffect()
    {
        ((InstanceRelicData)relicData).RemoveEffect();
    }

    public void Effect()
    {
        if (((InstanceRelicData)relicData).ConditionCheck())
        {
            ((InstanceRelicData)relicData).Effect();
            StartCoroutine(ConditionEffect());
        }
    }
}
