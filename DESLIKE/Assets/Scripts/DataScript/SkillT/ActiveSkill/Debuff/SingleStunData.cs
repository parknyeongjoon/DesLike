using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleStunData", menuName = "ScriptableObject/SkillT/SingleStunData")]
public class SingleStunData : SingleDebuffData
{
    public override IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }

        //���� ������ ����
        targetInfo.Stun(debuff_Time);
        Debug.Log("����");
        yield return null;
    }
}
