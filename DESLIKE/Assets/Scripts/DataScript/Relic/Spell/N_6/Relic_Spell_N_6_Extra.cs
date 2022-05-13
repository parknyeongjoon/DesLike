using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_6_Extra", menuName = "ScriptableObject/RelicT/Extra/Relic_Spell_N_6_Extra")]
public class Relic_Spell_N_6_Extra : SkillData
{
    [SerializeField] Mucus mucus;
    [SerializeField] Buff_Stat buff_Stat;
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if(heroInfo.mucus == mucus.mucusAmount)
        {
            heroInfo.buff_Stat.Add_Stat(buff_Stat);
        }
    }
}
