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
        RelicManager.instance.RemoveRelic(synergyRelic.code);//�ó��� ���� �ı��ϰ�
        RelicManager.instance.RemoveRelic(code);//�� ���� �ı��ϰ�
        RelicManager.instance.GetRelic(synergyRelic.code + "_Synergy");//�ó��� ���� �������ְ�
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
