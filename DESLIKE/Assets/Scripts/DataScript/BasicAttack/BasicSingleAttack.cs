using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleAttack : BasicAttack
{
    protected override IEnumerator Attack(CastleInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        //heroInfo.skeletonAnimation.state.SetAnimation(0, "att_1", false);//½ºÅ³
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
