using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfHeal", menuName = "ScriptableObject/ExtraSkill/SelfHeal")]
public class SelfHeal : SkillData
{
    public GameObject healEffect;
    public float heal_Amount;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.OnHealed(heal_Amount);
        excuteExtraSkill(heroInfo, targetInfo);
        heroInfo.StartCoroutine(Healing(heroInfo));
    }

    IEnumerator Healing(HeroInfo targetInfo)
    {
        GameObject createEffect;
        createEffect = Instantiate(healEffect, targetInfo.transform);
        yield return new WaitForSeconds(0.4f);
        Destroy(createEffect);
    }
}
