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
            heroInfo.skeletonAnimation.state.SetAnimation(0, heroInfo.castleData.code + "_Att_1", false);
            //AkSoundEngine.PostEvent(heroInfo.castleData.code + "_Att_1", gameObject);//활성화

            yield return new WaitForSeconds(basicAttackData.start_Delay);
            if (targetInfo && targetInfo.gameObject.layer != 7)
            {
                beforeAtkEvent?.Invoke(heroInfo, targetInfo);//공격 전 이벤트(ex. 데미지 연산)
                basicAttackData.Effect(heroInfo, targetInfo);//공격
                afterAtkEvent?.Invoke(heroInfo, targetInfo);//공격 후 이벤트(ex. 추가 효과 등)
                atkStack++;
                if (i >= atkCount + basicAttackData.atkCount - 1)//후딜레이는 마지막 한 번만
                {
                    heroInfo.action = Soldier_Action.End_Delay;
                    yield return new WaitForSeconds(basicAttackData.end_Delay);
                }
            }
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
