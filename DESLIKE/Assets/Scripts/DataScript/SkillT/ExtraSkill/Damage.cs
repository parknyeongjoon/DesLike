using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage",menuName ="ScriptableObject/ExtraSkill/Damage")]
public class Damage : SkillData
{
    public float damage;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.OnDamaged(heroInfo, damage);
    }
}
