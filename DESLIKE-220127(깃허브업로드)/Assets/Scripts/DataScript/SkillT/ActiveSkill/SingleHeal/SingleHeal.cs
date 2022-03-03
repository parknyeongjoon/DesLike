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
            ((SingleHealData)skillData).Effect(targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((SingleAttackData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
        if(heroInfo.cur_Mp < ((ActiveSkillData)skillData).mp)
        {
            heroInfo.state = Soldier_State.Charge;
        }
    }

    public override void Detect()
    {
        FindNearestSoldier(heroInfo.portDatas.spawnSoldierList);
    }

    void FindNearestSoldier(List<SoldierInfo> soldierInfos)//힐 가중치 + 거리 등등 다양한 요소 합산해서 구하기
    {
        float minDistance = 10e9F;
        CastleInfo targetInfo = null;
        for (int i = 0; i < soldierInfos.Count || i < 5; i++)
        {
            float distance = Vector3.Distance(this.transform.position, soldierInfos[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetInfo = soldierInfos[i];
            }
        }
        heroInfo.skillTargetInfo = targetInfo;
        heroInfo.skillTarget = targetInfo.gameObject;
        if(heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
        {
            heroInfo.state = Soldier_State.Battle;
        }
    }
}
