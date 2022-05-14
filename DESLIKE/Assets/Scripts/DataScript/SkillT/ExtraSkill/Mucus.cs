using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mucus", menuName = "ScriptableObject/ExtraSkill/Mucus")]
public class Mucus : SkillData
{
    public int mucusAmount;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        excuteExtraSkill(heroInfo, targetInfo);
        targetInfo.mucus += mucusAmount;
    }
}
