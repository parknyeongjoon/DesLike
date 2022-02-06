using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleAttack",menuName = "ScriptableObject/BasicAttack/BasicSingleAttack")]
public class BasicSingleAttackData : BasicAttackData
{
    public IEnumerator Effect(CastleInfo targetInfo, HeroInfo heroInfo)
    {
        yield return new WaitForSeconds(start_Delay);
        if (targetInfo)
        {
            targetInfo.OnDamaged(atk_Dmg);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
