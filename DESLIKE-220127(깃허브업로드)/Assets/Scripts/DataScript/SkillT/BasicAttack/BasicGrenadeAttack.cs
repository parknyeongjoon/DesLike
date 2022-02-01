using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrenadeAttack : MonoBehaviour
{
    public BasicGrenadeAttackData grenadeAtkData;

    protected SoldierBehaviour soldierBehaviour;
    protected HeroInfo heroInfo;

    protected void OnEnable()
    {
        soldierBehaviour = GetComponentInParent<SoldierBehaviour>();
        heroInfo = GetComponentInParent<HeroInfo>();

        soldierBehaviour.atkArea = (int)grenadeAtkData.atkArea * (int)heroInfo.team;
        soldierBehaviour.atkLayer = (int)grenadeAtkData.atkArea * 7;
        soldierBehaviour.atkRange = grenadeAtkData.range;
        soldierBehaviour.atkHandler += Effect;
    }

    protected void OnDisable()
    {
        soldierBehaviour.atkHandler -= Effect;
    }

    protected IEnumerator Effect()
    {
        while (heroInfo.TargetCheck(grenadeAtkData.range) && heroInfo.state != Soldier_State.Detect)
        {
            yield return new WaitForSeconds(grenadeAtkData.startDelay);
            if (heroInfo.targetInfo != null)
            {
                Collider2D[] targetColliders = Physics2D.OverlapCircleAll(heroInfo.target.transform.position, grenadeAtkData.extent, soldierBehaviour.atkArea ^ soldierBehaviour.atkLayer);
                //soldierBehaviour.animator.SetTrigger("isAtk");
                for (int i = 0; i < targetColliders.Length && i < grenadeAtkData.maxTarget; i++)
                {
                    targetColliders[i].GetComponent<CastleInfo>().OnDamaged(grenadeAtkData.atkDmg);
                }
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(grenadeAtkData.endDelay);
        }
        heroInfo.target = null;
        heroInfo.state = Soldier_State.Idle;
    }
}
