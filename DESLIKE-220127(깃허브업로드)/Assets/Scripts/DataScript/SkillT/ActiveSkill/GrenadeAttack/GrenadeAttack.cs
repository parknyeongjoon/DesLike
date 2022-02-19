using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttack : ActiveSkill
{
    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        Debug.Log("GrenadeAttack");
        yield return new WaitForSeconds(((GrenadeAttackData)skillData).start_Delay);
        if ((targetInfo && targetInfo.gameObject.layer != 7) || !targetInfo)
        {
            heroInfo.cur_Mp -= ((GrenadeAttackData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            heroInfo.animator.SetTrigger("isAtk");
            ((GrenadeAttackData)skillData).Effect(targetInfo, heroInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((GrenadeAttackData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}