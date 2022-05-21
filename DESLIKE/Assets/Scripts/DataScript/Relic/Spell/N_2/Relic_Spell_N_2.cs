using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_2", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_2")]
public class Relic_Spell_N_2 : InstanceRelicData
{
    [SerializeField] Dodge dodge;
    [SerializeField] float addP;

    public override void Effect()
    {
        dodge.dodgeP += addP;
    }

    public override void RemoveEffect()
    {
        dodge.dodgeP -= addP;
    }
}
