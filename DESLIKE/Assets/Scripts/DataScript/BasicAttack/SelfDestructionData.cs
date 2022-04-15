using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SelfDestruction",menuName ="ScriptableObject/BasicAttack/SelfDestruction")]
public class SelfDestructionData : BasicGrenadeAttackData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        base.Effect(heroInfo, targetInfo);
        heroInfo.Die();
    }
}
