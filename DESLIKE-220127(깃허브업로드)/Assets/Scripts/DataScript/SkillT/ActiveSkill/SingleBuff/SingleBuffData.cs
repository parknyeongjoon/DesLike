using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleBuffData", menuName = "ScriptableObject/SkillT/SingleBuffData")]
public class SingleBuffData : ActiveSkillData//�ۼ�Ʈ�� ���� �ִ� �� �����س���
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public bool isStack;

    public IEnumerator Add_Buff(HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        targetInfo.buff_Stat.Add_Stat(buff_Stat);
        Debug.Log("���� ����");
        yield return new WaitForSeconds(buff_Time);
        Remove_Buff(targetInfo);
    }

    public void Remove_Buff(HeroInfo targetInfo)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine.Remove(code);
        Debug.Log("���� ����");
    }
}
