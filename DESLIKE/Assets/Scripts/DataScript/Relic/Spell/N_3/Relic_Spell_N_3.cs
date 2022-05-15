using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_3", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_3")]
public class Relic_Spell_N_3 : InstanceRelicData//역병의 지속시간과 딜 간격 감소
{
    [SerializeField] DotDebuffData plagueData;
    [SerializeField] float debuffTimeP, dotTimeP;

    public override void Effect()
    {
        plagueData.debuff_Time *= debuffTimeP;
        plagueData.dotTime *= dotTimeP;
    }
}
