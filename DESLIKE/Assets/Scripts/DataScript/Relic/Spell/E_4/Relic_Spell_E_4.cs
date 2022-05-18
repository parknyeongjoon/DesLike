using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Relic_Spell_E_4",menuName ="ScriptableObject/RelicT/Spell/Relic_Spell_E_4")]
public class Relic_Spell_E_4 : HeroDataRelicData
{
    [SerializeField] SkillData extraSkillData;

    public override void relicEffect(HeroInfo heroInfo)
    {
        heroInfo.afterDeadEvent.AddListener(extraEffect);
    }

    public void extraEffect(HeroInfo heroInfo)//죽은 자리 주변 광역 데미지
    {
        extraSkillData.Effect(heroInfo, heroInfo);
    }

    public override bool ConditionCheck(HeroData heroData)
    {
        return true;
    }
}
