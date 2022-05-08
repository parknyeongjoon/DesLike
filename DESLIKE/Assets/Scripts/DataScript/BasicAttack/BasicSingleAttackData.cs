using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleAttack",menuName = "ScriptableObject/BasicAttack/BasicSingleAttack")]
public class BasicSingleAttackData : BasicAttackData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        ChargeMP(heroInfo);
        targetInfo.OnDamaged(heroInfo, atk_Dmg);
        extraSkillData?.Effect(heroInfo, targetInfo);
    }
}
