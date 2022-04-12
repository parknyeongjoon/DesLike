using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttack : ActiveSkill
{
    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        Debug.Log("GrenadeAttack");
        heroInfo.action = Soldier_Action.Skill;
        yield return new WaitForSeconds(((GrenadeAttackData)skillData).start_Delay);
        if ((targetInfo && targetInfo.gameObject.layer != 7) || !targetInfo)
        {
            heroInfo.cur_Mp -= ((GrenadeAttackData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            heroInfo.skeletonAnimation.state.SetAnimation(0, "skill_1", false);//스킬
            ((GrenadeAttackData)skillData).Effect(heroInfo, targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((GrenadeAttackData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}