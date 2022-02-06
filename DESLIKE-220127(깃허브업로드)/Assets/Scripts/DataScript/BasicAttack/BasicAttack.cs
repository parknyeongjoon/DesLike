using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public HeroInfo heroInfo;
    public BasicAttackData basicAttackData;
    protected SoldierBehaviour soldierBehaviour;

    public int atkArea, atkLayer;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
        soldierBehaviour = GetComponent<SoldierBehaviour>();
        atkArea = (int)heroInfo.team * (int)basicAttackData.atkArea;
        atkLayer = (int)basicAttackData.atkArea * 7;

        soldierBehaviour.atkDetect += Detect;
        soldierBehaviour.canAtk += CanAttackCheck;
    }

    protected void Detect()
    {
        Debug.Log("ÆòÅ¸ Å½»ö");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (CanAttackCheck())
            {
                Debug.Log("½Î¿ò½ÃÀÛ");
                if(heroInfo.target.tag == "Castle") { heroInfo.state = Soldier_State.Siege; }
                else { heroInfo.state = Soldier_State.Battle; }
                heroInfo.targetInfo = heroInfo.target.GetComponent<CastleInfo>();
            }
        }
    }

    public bool CanAttackCheck()
    {
        if (heroInfo.TargetCheck(basicAttackData.range))
        {
            return true;
        }
        return false;
    }
}
