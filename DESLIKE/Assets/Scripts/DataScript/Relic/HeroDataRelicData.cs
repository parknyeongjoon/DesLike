using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataRelicData : RelicData
{
    public virtual void Effect(HeroData heroData)//heroData.extraSkill�� �־������ heroInfo�� start���� relicEffect�� �ڵ����� ����
    {
        heroData.extraSkills += relicEffect;
    }

    public virtual void RemoveEffect(HeroData heroData)
    {
        heroData.extraSkills -= relicEffect;
    }

    public virtual void relicEffect(HeroInfo heroInfo)//heroInfo�� ��� �߰��� ���� �ۼ�
    {

    }

    public virtual bool ConditionCheck(HeroData heroData)
    {
        return false;
    }
}
