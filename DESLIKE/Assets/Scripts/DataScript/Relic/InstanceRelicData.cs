using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRelicData : RelicData
{
    public virtual void Effect()
    {

    }

    public virtual void RemoveEffect()
    {

    }

    public virtual bool ConditionCheck()
    {
        return true;
    }
}
