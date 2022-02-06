using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGrenadeAttack : BasicAttack
{
    public BasicGrenadeAttackData grenadeAtkData;

    protected override void Start()
    {
        base.Start();
    }

    public void BasicAttack(CastleInfo targetInfo)
    {
        StartCoroutine(((BasicGrenadeAttackData)basicAttackData).Effect(targetInfo, heroInfo));
    }
}
