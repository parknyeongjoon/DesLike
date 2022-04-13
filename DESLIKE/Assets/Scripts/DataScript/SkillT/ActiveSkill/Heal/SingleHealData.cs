using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleHealData",menuName ="ScriptableObject/SkillT/SingleHealData")]
public class SingleHealData : ActiveSkillData
{
    public GameObject healEffect;
    public float heal_Amount;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.OnHealed(heal_Amount);
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