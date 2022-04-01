using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleBuffData", menuName = "ScriptableObject/SkillT/SingleBuffData")]
public class SingleBuffData : ActiveSkillData//�ۼ�Ʈ�� ���� �ִ� �� �����س���
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public int max_Stack;

    public void Effect(HeroInfo targetInfo)//�̷� ������ ȿ���� ������ ����
    {
        targetInfo.buff_Stat.Add_Stat(buff_Stat);
    }

    public void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
