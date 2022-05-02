using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : ActiveSkill
{
    public override void Detect()
    {
        SetHealTarget(heroInfo.allyPortDatas.spawnSoldierList);
    }

    void SetHealTarget(List<HeroInfo> soldierInfos)//힐 가중치 + 거리 등등 다양한 요소 합산해서 구하기
    {
        float targetHealWeight = 0;
        HeroInfo targetInfo = null;
        for (int i = 0; i < soldierInfos.Count || i < 5; i++)
        {
            if (soldierInfos[i].healWeight > targetHealWeight && soldierInfos[i] != heroInfo)//힐 가중치가 높은 대상 찾기 && 자신 제외
            {
                targetHealWeight = soldierInfos[i].healWeight;
                targetInfo = soldierInfos[i];
            }
        }
        if(targetInfo != null)
        {
            heroInfo.skillTargetInfo = targetInfo;
            heroInfo.skillTarget = targetInfo.gameObject;
            if (heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
            {
                heroInfo.state = Soldier_State.Battle;
            }
        }
        else
        {
            heroInfo.skillTarget = heroInfo.allyPortDatas.spawnSoldierList[0].gameObject;
        }
    }
}
