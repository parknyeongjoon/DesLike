using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffData", menuName = "ScriptableObject/extraSkill/DebuffData")]
public class DebuffData : SkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        Coroutine tempCoroutine = targetInfo.StartCoroutine(DebuffCoroutine(heroInfo, targetInfo));
        targetInfo.debuffCoroutine[code].Add(tempCoroutine);
    }

    public virtual IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[code].Count >= max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[code][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
        }

        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        yield return new WaitForSeconds(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
    }

    protected virtual void Remove_Debuff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.debuffCoroutine[code].Remove(coroutine);
    }
}
