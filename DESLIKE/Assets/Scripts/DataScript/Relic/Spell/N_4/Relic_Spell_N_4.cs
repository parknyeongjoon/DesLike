using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_4", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_4")]
public class Relic_Spell_N_4 : InstanceRelicData
{
    [SerializeField] DotDebuffData plague;
    [SerializeField] DebuffData extraSkill;

    public override void Effect()
    {
        plague.extraSkillDatas.Add(extraSkill);
    }
}
