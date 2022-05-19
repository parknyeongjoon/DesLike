using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Spell_N_1 : InstanceRelicData
{
    public override void Effect()
    {

    }

    public override bool ConditionCheck()
    {
        return false;
        //Dictionary<string,RelicData>로 만들고 거기서 containsKey 사용하기
        //if(RelicManager.instance.relicList.)
    }
}
