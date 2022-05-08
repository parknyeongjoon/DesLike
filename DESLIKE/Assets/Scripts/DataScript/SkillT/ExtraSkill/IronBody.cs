using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IronBody",menuName ="ScriptableObject/ExtraSkill/IronBody")]
public class IronBody : SkillData
{
    public float reflectDmgP, reflectP;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.afterHitEvent += IronBodyEffect;
    }

    void IronBodyEffect(HeroInfo heroInfo, HeroInfo targetInfo, float damage)
    {
        int P = Random.Range(0, 100);
        if(P < reflectP)
        {
            Debug.Log("นÝป็");
            targetInfo.OnDamaged(reflectDmgP * damage);
        }
    }
}
