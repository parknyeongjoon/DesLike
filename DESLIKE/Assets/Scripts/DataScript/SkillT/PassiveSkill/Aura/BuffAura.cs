using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : Skill//������ �ٽ� ���ԵǸ� trigger if�� �ٽ� ��������
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
            yield return new WaitForSeconds(((AuraData)skillData).auraTerm);//�нú� ��ų ������ ����� ���� ��ų ��Ÿ�� �ֱ�
        }
    }
}
