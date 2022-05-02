using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : ActiveSkill
{
    List<HeroInfo> soldierList;

    protected override void Awake()
    {
        base.Awake();
        soldierList = heroInfo.enemyPortDatas.spawnSoldierList;
    }

    //singleDebuff�� Ÿ�Ͽ� �̹� ������ �ɷ��ִٸ� �ٸ� ��� ã��
    public override void Detect()
    {
        for (int i = 0; i < soldierList.Count; i++)//Awake���� ���� ���߿� ���� SoldierList ���ε��� �������ֱ�, ��Ʋ ���� ���� ���� �ֱ�?
        {
            if (soldierList[i] != heroInfo && !(soldierList[i].debuffCoroutine.ContainsKey(skillData.code) && soldierList[i].debuffCoroutine[skillData.code].Count < ((DebuffData)skillData).max_Stack))
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
