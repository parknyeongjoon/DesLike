using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleAttack : BasicAttack
{
    protected override IEnumerator Attack(CastleInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        yield return new WaitForSeconds(((BasicSingleAttackData)basicAttackData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.skeletonAnimation.state.SetAnimation(0, "att_1", false);//��ų
            AkSoundEngine.PostEvent("skeleton_Atk", gameObject);
            ((BasicSingleAttackData)basicAttackData).Effect(targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((BasicSingleAttackData)basicAttackData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
