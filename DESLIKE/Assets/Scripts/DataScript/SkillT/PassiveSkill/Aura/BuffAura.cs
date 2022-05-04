using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : Skill//공중전 다시 도입되면 trigger if문 다시 만져야함
{
    List<HeroInfo> effectList = new List<HeroInfo>();

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(UseSkill(null));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == heroInfo.gameObject.layer)
        {
            effectList.Add(collision.GetComponent<HeroInfo>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == heroInfo.gameObject.layer)
        {
            effectList.Remove(collision.GetComponent<HeroInfo>());
        }
    }

    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        yield return new WaitUntil(() => BattleUIManager.Instance.battleStart);
        while (true)
        {
            for (int i = 0; i < effectList.Count; i++)
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
            yield return new WaitForSeconds(((AuraData)skillData).auraTerm);//패시브 스킬 데이터 만들고 각각 스킬 쿨타임 넣기
        }
    }
}
