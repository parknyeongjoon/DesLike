using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDebuffData", menuName = "ScriptableObject/SkillT/SingleDebuffData")]
public class SingleDebuffData : ActiveSkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;

    public virtual void Effect(HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
    }

    public virtual void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
