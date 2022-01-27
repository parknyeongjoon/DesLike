using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleAttack : MonoBehaviour
{
    public BasicSingleAttackData atkData;

    protected SoldierBehaviour soldierBehaviour;
    protected HeroInfo heroInfo;

    protected virtual void OnEnable()
    {
        soldierBehaviour = GetComponentInParent<SoldierBehaviour>();
        heroInfo = GetComponentInParent<HeroInfo>();

        soldierBehaviour.atkArea = (int)atkData.atkArea * (int)heroInfo.team;
        soldierBehaviour.atkLayer = (int)atkData.atkArea * 7;
        soldierBehaviour.atkRange = atkData.range;
        soldierBehaviour.atkHandler += Effect;
    }

    protected void OnDisable()
    {
        soldierBehaviour.atkHandler -= Effect;
    }

    protected virtual IEnumerator Effect()
    {
        while (soldierBehaviour.TargetCheck(atkData.range) && heroInfo.state != State.Detect)
        {
            yield return new WaitForSeconds(atkData.startDelay);
            if (heroInfo.targetInfo != null)
            {
                //soldierBehaviour.animator.SetTrigger("isAtk");
                heroInfo.targetInfo.OnDamaged(atkData.atkDmg);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(atkData.endDelay);
        }
        heroInfo.target = null;
        heroInfo.state = State.Idle;
    }
}
