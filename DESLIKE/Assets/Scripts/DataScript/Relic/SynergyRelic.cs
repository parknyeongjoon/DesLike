using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SynergyRelic",menuName ="ScriptableObject/RelicT/SynergyRelic")]
public class SynergyRelic : InstanceRelicData
{
    [SerializeField] RelicData synergyRelic;
    [SerializeField] SkillData originSkill;

    public override void Effect()
    {
        RelicManager.instance.DestroyRelic(synergyRelic.code);//시너지 유물 파괴하고
        RelicManager.instance.DestroyRelic(code);//이 유물 파괴하고
        RelicManager.instance.GetRelic(synergyRelic.code + "_Synergy");//시너지 유물 생성해주고
    }

    public override bool ConditionCheck()
    {
        if (RelicManager.instance.relicList.ContainsKey(synergyRelic.code))
        {
            return true;
        }
        return false;
    }
}
