using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleAttackData",menuName = "ScriptableObject/SkillT/SingleAttackData")]
public class SingleAttackData : ActiveSkillData
{
    public float atk_Dmg;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        targetInfo.OnDamaged(atk_Dmg);
    }
}
