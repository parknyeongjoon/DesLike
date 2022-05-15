using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRelic : Relic
{
    void OnEnable()
    {
        Effect();
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
