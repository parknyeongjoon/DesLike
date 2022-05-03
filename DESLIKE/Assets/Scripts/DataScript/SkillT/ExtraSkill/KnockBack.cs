using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack", menuName = "ScriptableObject/ExtraSkill/KnockBack")]
public class KnockBack : SkillData
{
    public float knockBackDis, knockBackTime;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        Debug.Log("³Ë¹é");
        targetInfo.StartCoroutine(KnockBackEffect(heroInfo, targetInfo));
    }

    IEnumerator KnockBackEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        float time = 0;
        Vector3 startPos = targetInfo.transform.position;
        while(time <= knockBackTime)
        {
            time += Time.deltaTime;
            targetInfo.transform.position = Vector3.Lerp(startPos, startPos + (heroInfo.moveDir * knockBackDis), time / knockBackTime);
            yield return new WaitForFixedUpdate();
        }
    }
}