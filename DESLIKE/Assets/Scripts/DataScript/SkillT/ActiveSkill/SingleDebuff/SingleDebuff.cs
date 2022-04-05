using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDebuff : ActiveSkill
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.enemyPortDatas.spawnSoldierList;
    }

    //singleBuff�� Ÿ�Ͽ� �̹� ������ �ɷ��ִٸ� �ٸ� ��� ã��
    public override IEnumerator UseSkill(HeroInfo targetInfo)//�ڷ�ƾ�� monoBehaviour�� ��������
    {
        heroInfo.action = Soldier_Action.Skill;
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            //heroInfo.animator.SetTrigger("isAtk");
            if (!targetInfo.debuffCoroutine.ContainsKey(skillData.code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
            {
                targetInfo.debuffCoroutine.Add(skillData.code, new List<Coroutine>());
            }

            if (targetInfo.debuffCoroutine[skillData.code].Count >= ((SingleDebuffData)skillData).max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
            {
                StopCoroutine(targetInfo.debuffCoroutine[skillData.code][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
                ((SingleDebuffData)skillData).Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[skillData.code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
            }
            targetInfo.debuffCoroutine[skillData.code].Add(StartCoroutine(((SingleDebuffData)skillData).DebuffCoroutine(targetInfo)));//������ �����ϴٸ� ����ؼ� List<Coroutine>�� �ֱ�//���� �������ְ� heroInfo ���� ��ųʸ��� �־��ֱ�

            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
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

        for (int i = 0; i < soldierList.Count; i++)//Awake���� ���� ���߿� ���� SoldierList ���ε��� �������ֱ�
        {
            if (!(soldierList[i].debuffCoroutine.ContainsKey(skillData.code) && soldierList[i].debuffCoroutine[skillData.code].Count < ((SingleDebuffData)skillData).max_Stack))
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
