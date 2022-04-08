using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : ActiveSkill
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
            heroInfo.skeletonAnimation.state.SetAnimation(0, "skill_1", false);//스킬
            ((SingleAttackData)skillData).Effect(targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
