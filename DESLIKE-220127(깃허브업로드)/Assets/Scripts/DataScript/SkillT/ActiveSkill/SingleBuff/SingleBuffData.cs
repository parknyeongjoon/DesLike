using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuffData : ActiveSkillData//퍼센트로 버프 주는 법 생각해내기
{
    public Buff_Stat buff_Stat;
    public float buff_Time;

    public IEnumerator Add_Buff(HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
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
