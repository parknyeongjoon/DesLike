using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleAttackData",menuName = "ScriptableObject/SkillT/SingleAttackData")]
public class SingleAttackData : ActiveSkillData
{
    public override void Effect(CastleInfo targetInfo)
    {
        targetInfo.OnDamaged(atk_Dmg);
    }
}
