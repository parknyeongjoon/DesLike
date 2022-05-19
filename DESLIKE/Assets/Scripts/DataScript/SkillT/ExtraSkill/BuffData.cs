using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObject/ExtraSkill/BuffData")]
public class BuffData : SkillData//�ۼ�Ʈ�� ���� �ִ� �� �����س���
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public int max_Stack;
    public BuffType buffType;
    [System.NonSerialized]
    public string buffCode;//�����

    protected void OnEnable()
    {
        if (buffType == BuffType.None) { buffCode = code; }
        else { buffCode = buffType.ToString(); }
    }

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        Coroutine tempCoroutine = targetInfo.StartCoroutine(BuffCoroutine(heroInfo, targetInfo));
        targetInfo.buffCoroutine[code].Add(tempCoroutine);
    }

    public virtual IEnumerator BuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        CheckCoroutineList(targetInfo);

        targetInfo.buff_Stat.Add_Stat(buff_Stat);
        AddBuffPanel(targetInfo);

        yield return new WaitForSeconds(buff_Time);

        Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
        RemoveBuffPanel(targetInfo);
    }

    void CheckCoroutineList(HeroInfo targetInfo)
    {
        if (!targetInfo.buffCoroutine.ContainsKey(code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            targetInfo.buffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.buffCoroutine[code].Count >= max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
        {
            targetInfo.StopCoroutine(targetInfo.buffCoroutine[code][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
            Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
        }
    }

    void AddBuffPanel(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
    }

    void RemoveBuffPanel(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
    }

    public virtual void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
