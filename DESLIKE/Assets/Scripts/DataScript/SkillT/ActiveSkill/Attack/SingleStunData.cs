using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleStunData", menuName = "ScriptableObject/SkillT/SingleStunData")]
public class SingleStunData : SingleAttackData
{
    public float stun_Time;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.Stun(stun_Time);
    }
}
