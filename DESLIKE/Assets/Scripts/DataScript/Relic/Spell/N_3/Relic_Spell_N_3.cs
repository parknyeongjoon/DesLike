using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_3", menuName = "ScriptableObject/RelicT/Spell/Relic_Spell_N_3")]
public class Relic_Spell_N_3 : InstanceRelicData//������ ���ӽð��� �� ���� ����
{
    [SerializeField] DotDebuffData plagueData;
    [SerializeField] float debuffTimeP, dotTimeP;

    public override void Effect()
    {
        plagueData.debuff_Time *= debuffTimeP;
        plagueData.dotTime *= dotTimeP;
    }
}
