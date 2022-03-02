using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : ActiveSkill
{
    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Skill;
        yield return new WaitForSeconds(((SingleAttackData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((SingleAttackData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            heroInfo.animator.SetTrigger("isAtk");
            ((SingleAttackData)skillData).Effect(targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((SingleAttackData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
