using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public override void Detect()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.skillTarget = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
            {
                heroInfo.state = Soldier_State.Battle;
                heroInfo.skillTargetInfo = heroInfo.skillTarget.GetComponent<HeroInfo>();
            }
        }
    }
}
