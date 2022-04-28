using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ActiveSkill//우선 버프 대상 정할 방법 구하기(portDatas에서 a,b,c 진영으로 구분하기?)
{
    List<HeroInfo> soldierList;

    protected override void Awake()
    {
        base.Awake();
        soldierList = heroInfo.allyPortDatas.spawnSoldierList;
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
            if (soldierList[i] != heroInfo && !(soldierList[i].buffCoroutine.ContainsKey(skillData.code) && soldierList[i].buffCoroutine[skillData.code].Count < ((BuffData)skillData).max_Stack))
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
