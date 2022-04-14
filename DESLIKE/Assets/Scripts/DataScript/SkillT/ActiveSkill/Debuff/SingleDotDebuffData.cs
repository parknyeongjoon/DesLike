using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDotDebuffData", menuName = "ScriptableObject/SkillT/SingleDotDebuffData")]
public class SingleDotDebuffData : SingleDebuffData
{
    public float dotTime;
    public float dotDamage;

    public override IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        Debug.Log(debuffCode);
        if (!targetInfo.debuffCoroutine.ContainsKey(debuffCode))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            Debug.Log("����");
            targetInfo.debuffCoroutine.Add(debuffCode, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[debuffCode].Count >= max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[debuffCode][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[debuffCode][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
        }

        float _debuff_Time = debuff_Time;
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(debuffCode); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(debuffCode); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        while (_debuff_Time >= dotTime)
        {
            targetInfo.OnDamaged(dotDamage);
            yield return new WaitForSeconds(dotTime);
            _debuff_Time -= dotTime;
        }
        yield return new WaitForSeconds(_debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(debuffCode); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(debuffCode); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
    }
}
