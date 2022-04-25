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
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[code].Count >= max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[code][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�, ���� �Ͼ
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
        }

        float _debuff_Time = debuff_Time;
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        while (_debuff_Time >= dotTime)
        {
            targetInfo.OnDamaged(dotDamage);
            yield return new WaitForSeconds(dotTime);
            _debuff_Time -= dotTime;
        }
        yield return new WaitForSeconds(_debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
    }
}
