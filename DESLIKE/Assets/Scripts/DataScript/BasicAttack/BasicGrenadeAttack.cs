using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrenadeAttack : BasicAttack
{
    protected override IEnumerator Attack(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        yield return new WaitForSeconds(((BasicGrenadeAttackData)basicAttackData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.skeletonAnimation.state.SetAnimation(0, "att_1", false);//½ºÅ³
            ((BasicGrenadeAttackData)basicAttackData).Effect(heroInfo, targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((BasicGrenadeAttackData)basicAttackData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
