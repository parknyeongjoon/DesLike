using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDebuffData", menuName = "ScriptableObject/SkillT/SingleDebuffData")]
public class SingleDebuffData : ActiveSkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;

    public virtual IEnumerator DebuffCoroutine(HeroInfo targetInfo)
    {
        Effect(targetInfo);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        yield return new WaitForSeconds(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)
    }

    protected virtual void Effect(HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
    }

    public virtual void Remove_Debuff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.debuffCoroutine[code].Remove(coroutine);
    }
}
