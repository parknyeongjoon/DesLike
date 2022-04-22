using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAura : Skill
{
    List<HeroInfo> effectList = new List<HeroInfo>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != heroInfo.gameObject.layer)
        {
            effectList.Add(collision.GetComponent<HeroInfo>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer != heroInfo.gameObject.layer)
        {
            effectList.Remove(collision.GetComponent<HeroInfo>());
        }
    }

    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        for(int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i])
            {
                skillData.Effect(heroInfo, effectList[i]);
            }
            else
            {
                effectList.Remove(effectList[i]);
            }
        }
        yield return new WaitForSeconds(1.0f);//패시브 스킬 데이터 만들고 각각 스킬 쿨타임 넣기
    }
}
