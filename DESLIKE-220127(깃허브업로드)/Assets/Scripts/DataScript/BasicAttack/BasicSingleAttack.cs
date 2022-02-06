using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleAttack : BasicAttack
{
    protected override void Start()
    {
        base.Start();

        soldierBehaviour.atkHandler += BasicAttack;
    }

    public void BasicAttack(CastleInfo targetInfo)
    {
        StartCoroutine(((BasicSingleAttackData)basicAttackData).Effect(targetInfo, heroInfo));
    }
}
