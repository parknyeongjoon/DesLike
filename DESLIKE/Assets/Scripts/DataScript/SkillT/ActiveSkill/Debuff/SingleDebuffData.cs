using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDebuffData", menuName = "ScriptableObject/SkillT/SingleDebuffData")]
public class SingleDebuffData : ActiveSkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;
    public BuffType buffType;
    [System.NonSerialized]
    public string debuffCode;

    protected void OnEnable()
    {
        if(buffType == BuffType.None) { debuffCode = code; }
        else { debuffCode = buffType.ToString(); }
    }

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        Coroutine tempCoroutine = targetInfo.StartCoroutine(DebuffCoroutine(heroInfo, targetInfo));
        targetInfo.debuffCoroutine[debuffCode].Add(tempCoroutine);
    }

    public virtual IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if (!targetInfo.debuffCoroutine.ContainsKey(debuffCode))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            targetInfo.debuffCoroutine.Add(debuffCode, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[debuffCode].Count >= max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[debuffCode][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[debuffCode][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
        }

        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(debuffCode); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(debuffCode); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        yield return new WaitForSeconds(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(debuffCode); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(debuffCode); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[debuffCode][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
    }

    protected virtual void Remove_Debuff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.debuffCoroutine[debuffCode].Remove(coroutine);
    }
}
