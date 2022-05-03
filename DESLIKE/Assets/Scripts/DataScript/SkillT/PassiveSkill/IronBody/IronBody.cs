using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IronBody",menuName ="ScriptableObject/SkillT/IronBody")]
public class IronBody : SkillData
{
    public float reflectDmg;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.afterHitEvent += IronBodyEffect;
    }

    void IronBodyEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.OnDamaged(reflectDmg);
        Debug.Log("�ݻ�");
    }
}
