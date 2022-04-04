using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleBuffData", menuName = "ScriptableObject/SkillT/SingleBuffData")]
public class SingleBuffData : ActiveSkillData//�ۼ�Ʈ�� ���� �ִ� �� �����س���
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public int max_Stack;

    public virtual IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        Effect(targetInfo);
        yield return new WaitForSeconds(buff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
    }

    protected virtual void Effect(HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        targetInfo.buff_Stat.Add_Stat(buff_Stat);
    }

    public virtual void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
