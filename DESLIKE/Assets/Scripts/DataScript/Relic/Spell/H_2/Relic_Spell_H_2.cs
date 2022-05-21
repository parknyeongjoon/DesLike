using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Relic_Spell_H_2",menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_H_2")]
public class Relic_Spell_H_2 : InstanceRelicData
{
    [SerializeField] DotDebuffData plague;
    [SerializeField] Damage extraSkill;

    public override void Effect()
    {
        plague.stackOverflowEffect = extraSkill;
    }

    public override void RemoveEffect()
    {
        plague.stackOverflowEffect = null;
    }
}
