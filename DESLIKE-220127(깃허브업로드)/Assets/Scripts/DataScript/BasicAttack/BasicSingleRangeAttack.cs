using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleRangeAttack : BasicAttack
{
    protected override IEnumerator Attack(CastleInfo targetInfo)
    {
        yield return new WaitForSeconds(basicAttackData.start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.animator.SetTrigger("isAtk");
            ((BasicSingleRangeAttackData)basicAttackData).Effect(this, heroInfo.targetInfo, heroInfo.transform);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(basicAttackData.end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
