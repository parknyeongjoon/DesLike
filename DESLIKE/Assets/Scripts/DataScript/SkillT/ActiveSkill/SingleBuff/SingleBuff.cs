using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuff : ActiveSkill//�켱 ���� ��� ���� ��� ���ϱ�(portDatas���� a,b,c �������� �����ϱ�?)
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.portDatas.spawnSoldierList;
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
            if (!targetInfo.buffCoroutine.ContainsKey(skillData.code))//��ųʸ��� Ű�� ���ٸ� �ڷ�ƾ ����Ʈ �߰�
            {
                targetInfo.buffCoroutine.Add(skillData.code, new List<Coroutine>());
            }

            if (targetInfo.buffCoroutine[skillData.code].Count >= ((SingleBuffData)skillData).max_Stack)//�ִ� ���� �� ���� ���� �� �˻�
            {
                StopCoroutine(heroInfo.buffCoroutine[skillData.code][0]);//���� ������ �ڷ�ƾ ������Ű�� �����ϱ�
                ((SingleBuffData)skillData).Remove_Buff(targetInfo, targetInfo.buffCoroutine[skillData.code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)//ȿ�� �������ֱ�
            }
            targetInfo.buffCoroutine[skillData.code].Add(StartCoroutine(BuffCoroutine(targetInfo)));//������ �����ϴٸ� ����ؼ� List<Coroutine>�� �ֱ�//���� �������ְ� heroInfo ���� ��ųʸ��� �־��ֱ�

            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        ((SingleBuffData)skillData).Effect(targetInfo);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(skillData.code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(skillData.code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
        yield return new WaitForSeconds(((SingleBuffData)skillData).buff_Time);
        ((SingleBuffData)skillData).Remove_Buff(targetInfo, targetInfo.buffCoroutine[skillData.code][0]);//��ġ��(0��° �ε������� ����� �ڷ�ƾ�� ���� ����� ������?)
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(skillData.code); }//�������� ������ ��ٸ� ���� �г� ������Ʈ
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(skillData.code); }//���� soldierPanel���� �����ְ� �ִ� ������ ���� �г� ������Ʈ
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
            if (!(soldierList[i].buffCoroutine.ContainsKey(skillData.code) && soldierList[i].buffCoroutine[skillData.code].Count < ((SingleBuffData)skillData).max_Stack))
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
