using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Spell_N_5 : Relic//������ �Ǻ��� �������� n �����մϴ�.
{
    [SerializeField] Mucus mucusData;
    [SerializeField] int addAmount;

    void OnEnable()
    {

    }

    public override void Effect()
    {
        mucusData.mucusAmount += addAmount;
    }
}
