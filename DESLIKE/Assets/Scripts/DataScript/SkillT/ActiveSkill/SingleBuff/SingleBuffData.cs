using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleBuffData", menuName = "ScriptableObject/SkillT/SingleBuffData")]
public class SingleBuffData : ActiveSkillData//퍼센트로 버프 주는 법 생각해내기
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public int max_Stack;

    public virtual IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        Effect(targetInfo);
        yield return new WaitForSeconds(buff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)
    }

    protected virtual void Effect(HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        targetInfo.buff_Stat.Add_Stat(buff_Stat);
    }

    public virtual void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
