using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGrenadeAttack",menuName = "ScriptableObject/BasicAttack/BasicGrenadeAttack")]
public class BasicGrenadeAttackData : BasicSingleAttackData
{
    public int maxTarget;
    public float extent;
}
