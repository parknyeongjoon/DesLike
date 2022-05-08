using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ActiveSkillData",menuName ="ScriptableObject/SkillT/ActiveSkillData")]
public class ActiveSkillData : SkillData
{
    public float cooltime, mp, range, start_Delay, end_Delay;
    public AttackArea atkArea;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        extraSkillData.Effect(heroInfo, targetInfo);
    }
}
