using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_E_2", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_E_2")]
public class Relic_Spell_E_2 : InstanceRelicData
{
    [SerializeField] Dodge dodge;
    [SerializeField] BuffData extraSkill;

    public override void Effect()
    {
        dodge.extraSkillDatas.Add(extraSkill);
    }
}
