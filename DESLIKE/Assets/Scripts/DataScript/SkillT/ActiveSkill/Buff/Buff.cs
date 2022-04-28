using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : ActiveSkill//�켱 ���� ��� ���� ��� ���ϱ�(portDatas���� a,b,c �������� �����ϱ�?)
{
    List<HeroInfo> soldierList;

    protected override void Awake()
    {
        base.Awake();
        soldierList = heroInfo.allyPortDatas.spawnSoldierList;
    }

    public override void Detect()
    {
        if (skillData.skillType == SkillType.InstanceSkill)//instance ��ų �������� �־��� ��� �����ϱ�
        {
            if (IsActive())
            {
                heroInfo.skillTarget = gameObject;
                heroInfo.skillTargetInfo = heroInfo;
                heroInfo.state = Soldier_State.Battle;
            }
            return;
        }

        for (int i = 0; i < soldierList.Count; i++)//Awake���� ���� ���߿� ���� SoldierList ���ε��� �������ֱ�, ��Ʋ ���� ���� ���� �ֱ�?
        {
            if (soldierList[i] != heroInfo && !(soldierList[i].buffCoroutine.ContainsKey(skillData.code) && soldierList[i].buffCoroutine[skillData.code].Count < ((BuffData)skillData).max_Stack))
            {
                heroInfo.skillTarget = soldierList[i].gameObject;
                heroInfo.skillTargetInfo = soldierList[i];
                break;
            }
        }

        if (heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
        {
            heroInfo.state = Soldier_State.Battle;
        }
    }
}
