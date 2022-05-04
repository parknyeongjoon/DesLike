using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "ScriptableObject/ExtraSkill/Stun")]
public class Stun : SkillData
{
    public float stun_Time;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.Stun(stun_Time);
    }
}
