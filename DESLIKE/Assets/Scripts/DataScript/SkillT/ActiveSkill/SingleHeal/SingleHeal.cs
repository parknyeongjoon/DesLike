using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHeal : ActiveSkill
{
    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Skill;
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            heroInfo.animator.SetTrigger("isAtk");
            ((SingleHealData)skillData).Effect(this, targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

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
            if (soldierInfos[i].healWeight > targetHealWeight)
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
