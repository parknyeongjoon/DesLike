using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : ActiveSkill
{
    List<HeroInfo> soldierList;

    protected override void Awake()
    {
        base.Awake();
        soldierList = heroInfo.enemyPortDatas.spawnSoldierList;
    }

    //singleDebuff는 타켓에 이미 버프가 걸려있다면 다른 대상 찾기
    public override void Detect()
    {
        for (int i = 0; i < soldierList.Count; i++)//Awake에서 적용 군중에 따라 SoldierList 따로따로 적용해주기, 배틀 중일 때만 버프 주기?
        {
            if (soldierList[i] != heroInfo && !(soldierList[i].debuffCoroutine.ContainsKey(skillData.code) && soldierList[i].debuffCoroutine[skillData.code].Count < ((DebuffData)skillData).max_Stack))
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
