using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuff : ActiveSkill
{
    //singleBuff는 타켓에 이미 버프가 걸려있다면 다른 대상 찾기
    protected override IEnumerator UseSkill(HeroInfo targetInfo)//코루틴은 monoBehaviour로 가져가기
    {
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            //heroInfo.animator.SetTrigger("isAtk");
            if (targetInfo.buffCoroutine.ContainsKey(skillData.code) && !((SingleBuffData)skillData).isStack)
            {
                ((SingleBuffData)skillData).Remove_Buff(targetInfo);
            }
            targetInfo.buffCoroutine.Add(skillData.code, StartCoroutine(((SingleBuffData)skillData).Add_Buff(targetInfo)));//버프 실행해주고 heroInfo 버프 딕셔너리에 넣어주기
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
