using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSingleRangeAttack : BasicAttack
{
    int curAmmo;

    protected override void Start()
    {
        base.Start();
        curAmmo = ((BasicSingleRangeAttackData)basicAttackData).ammo;
    }

    public void BasicAttack(CastleInfo targetInfo)
    {
        StartCoroutine(((BasicSingleRangeAttackData)basicAttackData).Effect(this, targetInfo, heroInfo));
    }
}
