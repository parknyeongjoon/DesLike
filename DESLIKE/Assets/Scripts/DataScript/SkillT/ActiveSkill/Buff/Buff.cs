using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ActiveSkill//우선 버프 대상 정할 방법 구하기(portDatas에서 a,b,c 진영으로 구분하기?)
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.allyPortDatas.spawnSoldierList;
    }

    //singleBuff는 타켓에 이미 버프가 걸려있다면 다른 대상 찾기
    public override IEnumerator UseSkill(HeroInfo targetInfo)//코루틴은 monoBehaviour로 가져가기
    {
        heroInfo.action = Soldier_Action.Skill;
        string temp = "T_" + heroInfo.castleData.code + "_Skill_1";//밖으로 빼기
        AkSoundEngine.PostEvent(temp, gameObject);
        if(heroInfo.skeletonAnimation.skeleton != null)
            heroInfo.skeletonAnimation.state.SetAnimation(0, "H_23101_Skill_1", false);//스킬
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);

        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            skillData.Effect(heroInfo, targetInfo);

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

        for (int i = 0; i < soldierList.Count; i++)//Awake에서 적용 군중에 따라 SoldierList 따로따로 적용해주기, 배틀 중일 때만 버프 주기?
        {
            if (soldierList[i] != heroInfo && !(soldierList[i].buffCoroutine.ContainsKey(skillData.code) && soldierList[i].buffCoroutine[skillData.code].Count < ((SingleBuffData)skillData).max_Stack))
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
