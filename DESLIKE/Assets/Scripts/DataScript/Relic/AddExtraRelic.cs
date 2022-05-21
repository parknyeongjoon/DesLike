using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddExtraRelic", menuName = "ScriptableObject/RelicT/AddExtraRelic")]
public class AddExtraRelic : InstanceRelicData
{
    [SerializeField] SkillData originSkill;
    [SerializeField] SkillData[] extraSkill;

    public override void Effect()
    {
        for(int i = 0; i < extraSkill.Length; i++)
        {
            originSkill.extraSkillDatas.Add(extraSkill[i]);
        }
    }

    public override void RemoveEffect()
    {
        for(int i = 0; i < extraSkill.Length; i++)
        {
            originSkill.extraSkillDatas.Remove(extraSkill[i]);
        }
    }
}
