using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleRangeAttack : BasicSingleAttack
{
    public BasicSingleRangeAttackData rangeAtkData;//Info들에 data에도 적용할 수 있을 듯

    int curAmmo;

    protected override void OnEnable()
    {
        soldierBehaviour = GetComponentInParent<SoldierBehaviour>();
        heroInfo = GetComponentInParent<HeroInfo>();

        soldierBehaviour.atkArea = (int)rangeAtkData.atkArea * (int)heroInfo.team;
        soldierBehaviour.atkLayer = (int)rangeAtkData.atkArea * 7;
        soldierBehaviour.atkRange = rangeAtkData.range;
        soldierBehaviour.atkHandler += Effect;
        curAmmo = rangeAtkData.ammo;
    }

    protected override IEnumerator Effect()
    {
        while (soldierBehaviour.TargetCheck(rangeAtkData.range) && heroInfo.state != State.Detect)
        {
            if(curAmmo <= 0)
            {
                SetOtherBasicAttack();
                break;
            }

            yield return new WaitForSeconds(rangeAtkData.startDelay);

            if (heroInfo.targetInfo != null)
            {
                curAmmo--;
                float shotTime = 0.0f;
                GameObject createArrow;
                createArrow = Instantiate(rangeAtkData.arrow, transform);
                while (shotTime < rangeAtkData.arrowSpeed)
                {
                    shotTime += Time.deltaTime;
                    createArrow.transform.position = Vector2.Lerp(createArrow.transform.position, heroInfo.target.transform.position, shotTime / rangeAtkData.arrowSpeed);
                    yield return null;
                }
                Destroy(createArrow);
                //soldierBehaviour.animator.SetTrigger("isAtk");
                heroInfo.targetInfo.OnDamaged(rangeAtkData.atkDmg);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(rangeAtkData.endDelay);
        }
        heroInfo.target = null;
        heroInfo.state = State.Idle;
    }

    void SetOtherBasicAttack()//두 개로 분리하는 게 맞긴 할 듯
    {
        soldierBehaviour.atkArea = (int)atkData.atkArea * (int)heroInfo.team;
        soldierBehaviour.atkLayer = (int)atkData.atkArea * 7;
        soldierBehaviour.atkRange = atkData.range;
        soldierBehaviour.atkHandler -= Effect;
        soldierBehaviour.atkHandler += base.Effect;
    }
}
