using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SynergiedRelic", menuName = "ScriptableObject/RelicT/SynergiedRelic")]
public class SynergiedRelic : InstanceRelicData
{
    [SerializeField] RelicData synergyRelic;
    [SerializeField] SkillData originSkill;
    [SerializeField] SkillData extraSkill;

    public override void Effect()
    {
        originSkill.extraSkillDatas.Add(extraSkill);
        if (RelicManager.Instance.relicList.ContainsKey(synergyRelic.code))
        {
            RelicManager.instance.relicList[synergyRelic.code].DoEffect();
        }
    }

    public override void RemoveEffect()
    {
        originSkill.extraSkillDatas.Remove(extraSkill);
    }

    public override bool ConditionCheck()
    {
        return true;
    }
}
