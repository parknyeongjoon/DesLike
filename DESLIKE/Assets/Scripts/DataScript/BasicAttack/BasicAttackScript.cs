using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackScript : BasicAttack
{
    protected override IEnumerator Attack(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Attack;
        for (int i = 0; i < atkCount + basicAttackData.atkCount; i++)//����Ƚ����ŭ �ݺ�
        {
            heroInfo.skeletonAnimation.state.SetAnimation(0, heroInfo.castleData.code + "_Att_1", false);
            //AkSoundEngine.PostEvent(heroInfo.castleData.code + "_Att_1", gameObject);//Ȱ��ȭ

            yield return new WaitForSeconds(basicAttackData.start_Delay);
            if (targetInfo && targetInfo.gameObject.layer != 7)
            {
                beforeAtkEvent?.Invoke(heroInfo, targetInfo);//���� �� �̺�Ʈ(ex. ������ ����)
                basicAttackData.Effect(heroInfo, targetInfo);//����
                afterAtkEvent?.Invoke(heroInfo, targetInfo);//���� �� �̺�Ʈ(ex. �߰� ȿ�� ��)
                atkStack++;
                if (i >= atkCount + basicAttackData.atkCount - 1)//�ĵ����̴� ������ �� ����
                {
                    heroInfo.action = Soldier_Action.End_Delay;
                    yield return new WaitForSeconds(basicAttackData.end_Delay);
                }
            }
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
