using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic_Spell_N_5 : Relic//점액질 피부의 내구도가 n 증가합니다.
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
