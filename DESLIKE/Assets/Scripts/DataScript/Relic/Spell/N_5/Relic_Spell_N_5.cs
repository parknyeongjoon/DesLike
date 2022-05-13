using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_5", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_5")]
public class Relic_Spell_N_5 : InstanceRelicData//������ �Ǻ��� �������� n �����մϴ�.
{
    [SerializeField] Mucus mucusData;
    [SerializeField] int addAmount;

    public override void Effect()
    {
        mucusData.mucusAmount += addAmount;
    }
}
