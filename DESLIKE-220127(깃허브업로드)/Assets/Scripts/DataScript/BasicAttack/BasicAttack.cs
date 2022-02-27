using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    protected HeroInfo heroInfo;
    public BasicAttackData basicAttackData;
    protected SoldierBasic soldierBasic;

    public int atkArea, atkLayer;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
        soldierBasic = GetComponent<SoldierBasic>();

        atkArea = (int)heroInfo.team * (int)basicAttackData.atkArea;
        atkLayer = (int)basicAttackData.atkArea * 7;

        soldierBasic.atkDetect += Detect;
        soldierBasic.canAtk += CanAttackCheck;
        soldierBasic.atkHandler += Attack;
    }

    protected void Detect()
    {
        Debug.Log("ÆòÅ¸ Å½»ö");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(heroInfo.target, basicAttackData.range + 2))
            {
                Debug.Log("½Î¿ò½ÃÀÛ");
                if(heroInfo.target.tag == "Castle") { heroInfo.state = Soldier_State.Siege; }
                else { heroInfo.state = Soldier_State.Battle; }
                heroInfo.targetInfo = heroInfo.target.GetComponent<CastleInfo>();
            }
        }
    }

    protected bool CanAttackCheck()
    {
        if (!heroInfo.targetInfo || heroInfo.target.layer == 7)
        {
            heroInfo.state = Soldier_State.Idle;
            return false;
        }
        if (heroInfo.TargetCheck(heroInfo.target, basicAttackData.range + heroInfo.targetInfo.castleData.size))
        {
            return true;
        }
        return false;
    }

    protected virtual IEnumerator Attack(CastleInfo targetInfo)
    {
        yield return null;
    }
}
