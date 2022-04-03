using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuff : ActiveSkill//우선 버프 대상 정할 방법 구하기(portDatas에서 a,b,c 진영으로 구분하기?)
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.portDatas.spawnSoldierList;
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
            if (!targetInfo.buffCoroutine.ContainsKey(skillData.code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
            {
                targetInfo.buffCoroutine.Add(skillData.code, new List<Coroutine>());
            }

            if (targetInfo.buffCoroutine[skillData.code].Count >= ((SingleBuffData)skillData).max_Stack)//최대 스택 수 보다 많은 지 검사
            {
                StopCoroutine(heroInfo.buffCoroutine[skillData.code][0]);//제일 오래된 코루틴 정지시키고 갱신하기
                ((SingleBuffData)skillData).Remove_Buff(targetInfo, targetInfo.buffCoroutine[skillData.code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
            }
            targetInfo.buffCoroutine[skillData.code].Add(StartCoroutine(BuffCoroutine(targetInfo)));//스택이 가능하다면 계속해서 List<Coroutine>에 넣기//버프 실행해주고 heroInfo 버프 딕셔너리에 넣어주기

            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        ((SingleBuffData)skillData).Effect(targetInfo);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(skillData.code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(skillData.code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        yield return new WaitForSeconds(((SingleBuffData)skillData).buff_Time);
        ((SingleBuffData)skillData).Remove_Buff(targetInfo, targetInfo.buffCoroutine[skillData.code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(skillData.code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(skillData.code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
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
            if (!(soldierList[i].buffCoroutine.ContainsKey(skillData.code) && soldierList[i].buffCoroutine[skillData.code].Count < ((SingleBuffData)skillData).max_Stack))
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
