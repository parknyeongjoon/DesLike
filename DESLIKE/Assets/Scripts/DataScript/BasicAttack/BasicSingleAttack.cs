using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleAttack : BasicAttack
{
    protected override IEnumerator Attack(CastleInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        if (heroInfo.skeletonAnimation.skeleton != null)
            heroInfo.skeletonAnimation.state.SetAnimation(0, "H_" + heroInfo.castleData.code + "_Att_1", false);//��ų
        string temp = "T_" + heroInfo.castleData.code + "_atk_1";
        AkSoundEngine.PostEvent(temp, gameObject);
        yield return new WaitForSeconds(((BasicSingleAttackData)basicAttackData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            ((BasicSingleAttackData)basicAttackData).Effect(targetInfo);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((BasicSingleAttackData)basicAttackData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
