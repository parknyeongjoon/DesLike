using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "ScriptableObject/ExtraSkill/Dodge")]
public class Dodge : SkillData
{
    public float dodgeP;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.beforeHitEvent += DodgeEffect;
    }

    void DodgeEffect(HeroInfo heroInfo, HeroInfo targetInfo, ref float damage)
    {
        int temp = Random.Range(0, 100);
        if(temp < dodgeP * 100)
        {
            Debug.Log("È¸ÇÇ");
            damage = 0;
            excuteExtraSkill(heroInfo, targetInfo);
        }
    }
}
