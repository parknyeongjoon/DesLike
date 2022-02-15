using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuffData : ActiveSkillData//�ۼ�Ʈ�� ���� �ִ� �� �����س���
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public bool isStack;

    public IEnumerator Add_Buff(HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        targetInfo.buff_Stat.Add_Stat(buff_Stat);
        yield return new WaitForSeconds(buff_Time);
        Remove_Buff(targetInfo);
    }

    public void Remove_Buff(HeroInfo targetInfo)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine.Remove(code);
    }
}
