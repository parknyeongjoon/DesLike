using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_H_3_Extra", menuName = "ScriptableObject/RelicT/Extra/Relic_Spell_H_3_Extra")]
public class Relic_Spell_H_3_Extra : SkillData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//Ÿ���� mucus�� �����ٸ� ������ �߰����ְ� mucus�� 0���� �˻����ִ� �Լ��� �ǰ��Լ��� �߰�
    {
        if (targetInfo.mucus == 0)
        {
            targetInfo.afterHitEvent += RemoveEffect;//�ǰ� �̺�Ʈ ������
        }
    }

    void RemoveEffect(HeroInfo heroInfo, HeroInfo targetInfo, float damage)//�ǰ� �̺�Ʈ
    {
        if (heroInfo.mucus == 0)
        {
            excuteExtraSkill(heroInfo, heroInfo);
            targetInfo.afterHitEvent -= RemoveEffect;
        }
    }
}
