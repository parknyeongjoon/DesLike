using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuff : ActiveSkill
{
    //singleBuff는 타켓에 이미 버프가 걸려있다면 다른 대상 찾기
    public override IEnumerator UseSkill(HeroInfo targetInfo)//코루틴은 monoBehaviour로 가져가기
    {
        heroInfo.action = Soldier_Action.Skill;
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            //heroInfo.animator.SetTrigger("isAtk");
            if (targetInfo.buffCoroutine.ContainsKey(skillData.code) && !((SingleBuffData)skillData).isStack)//스택이 가능하다면 dictionary는 키 중복 오류 일어날듯
            {
                StopCoroutine(heroInfo.buffCoroutine[skillData.code]);
                ((SingleBuffData)skillData).Remove_Buff(targetInfo);
            }
            targetInfo.buffCoroutine.Add(skillData.code, StartCoroutine(BuffCoroutine(targetInfo)));//버프 실행해주고 heroInfo 버프 딕셔너리에 넣어주기
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        ((SingleBuffData)skillData).Effect(targetInfo);
        yield return new WaitForSeconds(((SingleBuffData)skillData).buff_Time);
        ((SingleBuffData)skillData).Remove_Buff(targetInfo);
    }

    public override void Detect()
    {
        if (skillData.skillType == SkillType.InstanceSkill)//instance 스킬 병사한테 넣어줄 방법 생각하기
        {
            heroInfo.skillTarget = gameObject;
            heroInfo.skillTargetInfo = heroInfo;
            if (CanSkillCheck()) { heroInfo.state = Soldier_State.Battle; }
            return;
        }
        base.Detect();
    }
}
