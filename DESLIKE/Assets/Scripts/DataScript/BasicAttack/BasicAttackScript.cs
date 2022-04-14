using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackScript : BasicAttack
{
    protected override IEnumerator Attack(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        for (int i = 0; i < atkCount + basicAttackData.atkCount; i++)//공격횟수만큼 반복
        {
            Debug.Log(i + "attack");
            if (heroInfo.skeletonAnimation.skeleton != null)//스켈레톤 다 할당해주고 지우기
                heroInfo.skeletonAnimation.state.SetAnimation(0, "H_" + heroInfo.castleData.code + "_Att_1", false);
            string temp = "T_" + heroInfo.castleData.code + "_atk_1";
            AkSoundEngine.PostEvent(temp, gameObject);

            yield return new WaitForSeconds(basicAttackData.start_Delay);
            if (targetInfo && targetInfo.gameObject.layer != 7)
            {
                basicAttackData.Effect(heroInfo, targetInfo);//공격
                if (i == atkCount + basicAttackData.atkCount - 1)//후딜레이는 마지막 한 번만
                {
                    heroInfo.action = Soldier_Action.End_Delay;
                    yield return new WaitForSeconds(basicAttackData.end_Delay);
                }
            }
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
