using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDebuff : ActiveSkill
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.enemyPortDatas.spawnSoldierList;
    }

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
            if (!targetInfo.debuffCoroutine.ContainsKey(skillData.code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
            {
                targetInfo.debuffCoroutine.Add(skillData.code, new List<Coroutine>());
            }

            if (targetInfo.debuffCoroutine[skillData.code].Count >= ((SingleDebuffData)skillData).max_Stack)//최대 스택 수 보다 많은 지 검사
            {
                StopCoroutine(targetInfo.debuffCoroutine[skillData.code][0]);//제일 오래된 코루틴 정지시키고 갱신하기
                ((SingleDebuffData)skillData).Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[skillData.code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
            }
            targetInfo.debuffCoroutine[skillData.code].Add(StartCoroutine(((SingleDebuffData)skillData).DebuffCoroutine(targetInfo)));//스택이 가능하다면 계속해서 List<Coroutine>에 넣기//버프 실행해주고 heroInfo 버프 딕셔너리에 넣어주기

            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    public override void Detect()
    {
        if (skillData.skillType == SkillType.InstanceSkill)//instance 스킬 병사한테 넣어줄 방법 생각하기
        {
            if (IsActive())
            {
                heroInfo.skillTarget = gameObject;
                heroInfo.skillTargetInfo = heroInfo;
                heroInfo.state = Soldier_State.Battle;
            }
            return;
        }

        for (int i = 0; i < soldierList.Count; i++)//Awake에서 적용 군중에 따라 SoldierList 따로따로 적용해주기
        {
            if (!(soldierList[i].debuffCoroutine.ContainsKey(skillData.code) && soldierList[i].debuffCoroutine[skillData.code].Count < ((SingleDebuffData)skillData).max_Stack))
            {
                heroInfo.skillTarget = soldierList[i].gameObject;
                heroInfo.skillTargetInfo = soldierList[i];
                break;
            }
        }

        if (heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
        {
            heroInfo.state = Soldier_State.Battle;
        }
    }
}
