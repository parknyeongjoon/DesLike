using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddExtraRelic", menuName = "ScriptableObject/RelicT/AddExtraRelic")]
public class AddExtraRelic : InstanceRelicData
{
    [SerializeField] SkillData originSkill;
    [SerializeField] SkillData extraSkill;

    public override void Effect()
    {
        originSkill.extraSkillDatas.Add(extraSkill);
    }
}
