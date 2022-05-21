using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_E_3", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_E_3")]
public class Relic_Spell_E_3 : InstanceRelicData//������ �ִ� ���� �� ����.
{
    [SerializeField] DotDebuffData plague;
    [SerializeField] int addAmount;

    public override void Effect()
    {
        plague.max_Stack += addAmount;
    }

    public override void RemoveEffect()
    {
        plague.max_Stack -= addAmount;
    }
}
