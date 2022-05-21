using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_E_3", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_E_3")]
public class Relic_Spell_E_3 : InstanceRelicData//역병의 최대 스택 수 증가.
{
    [SerializeField] DotDebuffData plague;
    [SerializeField] int addAmount;

    public override void Effect()
    {
        plague.max_Stack += addAmount;
    }

    public override void RemoveEffect()
    {
        plague.max_Stack -= addAmount;
    }
}
