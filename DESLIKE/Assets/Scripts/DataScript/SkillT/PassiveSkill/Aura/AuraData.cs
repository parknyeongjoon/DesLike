using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AuraData",menuName ="ScriptableObject/SkillT/AuraData")]
public class AuraData : SkillData
{
    public float auraTerm;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        extraSkillData.Effect(heroInfo, targetInfo);
    }
}
