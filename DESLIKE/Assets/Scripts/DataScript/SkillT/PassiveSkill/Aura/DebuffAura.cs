using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAura : Skill//������ �ٽ� ���ԵǸ� trigger if�� �ٽ� ��������
{
    List<HeroInfo> effectList = new List<HeroInfo>();

    protected override void Start()
    {
        StartCoroutine(UseSkill(null));
    }

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
        Debug.Log(effectList.Count);
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
        yield return new WaitForSeconds(((AuraData)skillData).auraTerm);//�нú� ��ų ������ ����� ���� ��ų ��Ÿ�� �ֱ�
    }
}
