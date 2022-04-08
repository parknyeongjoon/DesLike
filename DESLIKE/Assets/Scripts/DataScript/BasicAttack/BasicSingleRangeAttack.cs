using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleRangeAttack : BasicAttack
{
    protected override IEnumerator Attack(CastleInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        yield return new WaitForSeconds(basicAttackData.start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.skeletonAnimation.state.SetAnimation(0, "att_1", false);//��ų
            ((BasicSingleRangeAttackData)basicAttackData).Effect(this, heroInfo.targetInfo, heroInfo.transform);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(basicAttackData.end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
