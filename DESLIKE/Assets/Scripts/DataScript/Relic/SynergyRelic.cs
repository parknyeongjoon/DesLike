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
        RelicManager.instance.DestroyRelic(synergyRelic.code);//H_1 �ı��ϰ�
        RelicManager.instance.GetRelic(synergyRelic.code + "_Synergy");//�ó��� ���� �������ְ�
        originSkill.extraSkillDatas.Add(extraSkill);//�߰�ȿ�� �ο�
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
