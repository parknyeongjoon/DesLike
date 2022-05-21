using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SynergyRelic",menuName ="ScriptableObject/RelicT/SynergyRelic")]
public class SynergyRelic : InstanceRelicData
{
    [SerializeField] RelicData synergyRelic;
    [SerializeField] SkillData originSkill;
    [SerializeField] SkillData extraSkill;

    public override void Effect()
    {
        RelicManager.instance.DestroyRelic(synergyRelic.code);//H_1 파괴하고
        RelicManager.instance.GetRelic(synergyRelic.code + "_Synergy");//시너지 유물 생성해주고
        originSkill.extraSkillDatas.Add(extraSkill);//추가효과 부여
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
