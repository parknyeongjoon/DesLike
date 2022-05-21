using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoneHand", menuName = "ScriptableObject/ExtraSkill/StoneHand")]
public class StoneHand : SkillData
{
    public float stunP;
    public float stun_Time;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        int temp = Random.Range(0, 100);
        if(temp < stunP * 100)
        {
            targetInfo.Stun(stun_Time);
        }
    }
}
