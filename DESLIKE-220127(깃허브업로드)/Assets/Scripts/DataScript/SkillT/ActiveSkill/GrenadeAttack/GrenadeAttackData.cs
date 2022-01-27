using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrenadeAttackData", menuName = "ScriptableObject/SkillT/GrenadeAttackData")]
public class GrenadeAttackData : ActiveSkillData
{
    public int max_Target;
    public float extent;

    public override void Effect(CastleInfo[] targetInfos)
    {
        for (int i = 0; i < targetInfos.Length; i++)
        {
            if (targetInfos[i] != null)
            {
                targetInfos[i].OnDamaged(atk_Dmg);
            }
        }
    }
}
