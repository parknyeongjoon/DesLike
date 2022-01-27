using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleAttack",menuName = "ScriptableObject/BasicAttack/BasicSingleAttack")]
public class BasicSingleAttackData : ScriptableObject
{
    public float startDelay, endDelay, range, atkDmg;
    public Atk_Type atk_Type;
    public AttackArea atkArea;
}
