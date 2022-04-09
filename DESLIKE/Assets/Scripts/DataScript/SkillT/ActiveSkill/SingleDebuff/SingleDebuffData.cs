using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDebuffData", menuName = "ScriptableObject/SkillT/SingleDebuffData")]
public class SingleDebuffData : ActiveSkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;

    public virtual IEnumerator DebuffCoroutine(HeroInfo targetInfo)
    {
        Effect(targetInfo);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        yield return new WaitForSeconds(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
    }

    protected virtual void Effect(HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
    }

    public virtual void Remove_Debuff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.debuffCoroutine[code].Remove(coroutine);
    }
}
