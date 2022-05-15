using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_6", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_6")]
public class Relic_Spell_N_6 : InstanceRelicData
{
    [SerializeField] SkillData extraSkill;
    [SerializeField] Mucus mucus;

    public override void Effect()
    {
        mucus.extraSkillDatas.Add(extraSkill);
    }
}
