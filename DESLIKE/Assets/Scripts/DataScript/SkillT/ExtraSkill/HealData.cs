using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealData",menuName ="ScriptableObject/ExtraSkill/HealData")]
public class HealData : SkillData
{
    public GameObject healEffect;
    public float heal_Amount;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.OnHealed(heal_Amount);
        extraSkillData.Effect(heroInfo, targetInfo);
        targetInfo.StartCoroutine(Healing(targetInfo));
    }

    IEnumerator Healing(HeroInfo targetInfo)
    {
        GameObject createBlood;
        createBlood = Instantiate(healEffect, targetInfo.transform);
        yield return new WaitForSeconds(0.4f);
        Destroy(createBlood);
    }
}