using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public HeroInfo heroInfo;
    public BasicAttackData basicAttackData;
    public SoldierBehaviour soldierBehaviour;

    public int atkArea, atkLayer;

    protected virtual void Start()
    {
        atkArea = (int)heroInfo.team * (int)basicAttackData.atkArea;
        atkLayer = (int)basicAttackData.atkArea * 7;

        soldierBehaviour.atkDetect += Detect;
        soldierBehaviour.canAtk += CanAttackCheck;
        soldierBehaviour.atkHandler += Attack;
    }

    protected void Detect()
    {
        Debug.Log("ÆòÅ¸ Å½»ö");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(basicAttackData.range + 2))
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
        if (heroInfo.TargetCheck(basicAttackData.range + heroInfo.targetInfo.castleData.size))
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
