using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_H_3_Extra", menuName = "ScriptableObject/RelicT/Extra/Relic_Spell_H_3_Extra")]
public class Relic_Spell_H_3_Extra : SkillData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//타켓의 mucus가 없었다면 버프를 추가해주고 mucus가 0인지 검사해주는 함수를 피격함수에 추가
    {
        if (targetInfo.mucus == 0)
        {
            targetInfo.afterHitEvent += RemoveEffect;//피격 이벤트 더해줌
        }
    }

    void RemoveEffect(HeroInfo heroInfo, HeroInfo targetInfo, float damage)//피격 이벤트
    {
        if (heroInfo.mucus == 0)
        {
            excuteExtraSkill(heroInfo, heroInfo);
            targetInfo.afterHitEvent -= RemoveEffect;
        }
    }
}
