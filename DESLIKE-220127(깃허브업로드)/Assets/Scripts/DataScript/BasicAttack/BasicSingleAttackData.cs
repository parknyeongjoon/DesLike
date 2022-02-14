using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleAttack",menuName = "ScriptableObject/BasicAttack/BasicSingleAttack")]
public class BasicSingleAttackData : BasicAttackData
{
    public void Effect(CastleInfo targetInfo)
    {
        targetInfo.OnDamaged(atk_Dmg);
    }
}
