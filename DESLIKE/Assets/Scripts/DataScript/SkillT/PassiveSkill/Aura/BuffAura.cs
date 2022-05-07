using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : Skill//������ �ٽ� ���ԵǸ� trigger if�� �ٽ� ��������
{
    List<HeroInfo> effectList = new List<HeroInfo>();
    public int atkArea;

    protected override void Awake()
    {
        StartCoroutine(UseSkill(null));
        atkArea = gameObject.layer;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Soldier") || collision.CompareTag("Player")) && collision.gameObject.layer == atkArea)
        {
            effectList.Add(collision.GetComponent<HeroInfo>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Soldier") || collision.CompareTag("Player")) && collision.gameObject.layer == atkArea)
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
                if (effectList[i] && effectList[i].gameObject.layer != 7)
                {
                    skillData.Effect(null, effectList[i]);
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