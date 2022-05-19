using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_E_6", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_E_6")]
public class Relic_Spell_E_6 : HeroDataRelicData
{
    [SerializeField] float activeP;
    [SerializeField] Mucus mucus;

    public override void relicEffect(HeroInfo heroInfo)
    {
        heroInfo.healthChangeEvent.AddListener(extraEffect);
    }

    public void extraEffect(HeroInfo heroInfo)
    {
        if(heroInfo.cur_Hp <= heroInfo.castleData.hp * activeP)
        {
            mucus.Effect(heroInfo, heroInfo);
            heroInfo.healthChangeEvent.RemoveListener(extraEffect);
        }
    }

    public override bool ConditionCheck(HeroData heroData)
    {
        return true;
    }
}
