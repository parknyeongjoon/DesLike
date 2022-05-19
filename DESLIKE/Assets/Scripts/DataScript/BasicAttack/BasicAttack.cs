using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicAttack : MonoBehaviour
{
    protected HeroInfo heroInfo;
    public BasicAttackData basicAttackData;
    protected SoldierBasic soldierBasic;

    public Action<HeroInfo, HeroInfo> beforeAtkEvent;
    public Action<HeroInfo, HeroInfo> afterAtkEvent;

    public int atkArea, atkLayer, atkCount;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
        soldierBasic = GetComponent<SoldierBasic>();

        atkArea = (int)heroInfo.team * (int)basicAttackData.atkArea;
        atkLayer = (int)basicAttackData.atkArea * 7;

        soldierBasic.atkDetect = Detect;
        soldierBasic.canAtk = CanAttackCheck;
        soldierBasic.atkHandler = Attack;
    }

    protected virtual void Detect()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(heroInfo.target, basicAttackData.range + 2))
            {
                heroInfo.state = Soldier_State.Battle;
                heroInfo.targetInfo = heroInfo.target.GetComponent<HeroInfo>();
            }
        }
    }

    protected virtual bool CanAttackCheck()
    {
        if (!heroInfo.targetInfo || heroInfo.target.layer == 7)
        {
            heroInfo.state = Soldier_State.Idle;
            heroInfo.action = Soldier_Action.Idle;
            return false;
        }
        if (heroInfo.TargetCheck(heroInfo.target, basicAttackData.range + heroInfo.targetInfo.castleData.size))
        {
            return true;
        }
        return false;
    }

    protected virtual IEnumerator Attack(HeroInfo targetInfo)
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
