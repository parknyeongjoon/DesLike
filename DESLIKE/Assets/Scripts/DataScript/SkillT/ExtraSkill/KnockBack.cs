using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack", menuName = "ScriptableObject/ExtraSkill/KnockBack")]
public class KnockBack : SkillData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.transform.position += heroInfo.moveDir * 2;//숫자 변경
    }
}
