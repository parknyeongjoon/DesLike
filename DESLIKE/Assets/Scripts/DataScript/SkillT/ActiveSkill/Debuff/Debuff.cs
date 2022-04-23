using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : ActiveSkill
{
    List<HeroInfo> soldierList;

    protected override void Start()
    {
        base.Start();
        soldierList = heroInfo.enemyPortDatas.spawnSoldierList;
    }

    //singleDebuff�� Ÿ�Ͽ� �̹� ������ �ɷ��ִٸ� �ٸ� ��� ã��
    public override IEnumerator UseSkill(HeroInfo targetInfo)//�ڷ�ƾ�� monoBehaviour�� ��������
    {
        heroInfo.action = Soldier_Action.Skill;
        //AkSoundEngine.PostEvent(skillData.code, gameObject);Ȱ��ȭ
        if (heroInfo.skeletonAnimation.skeleton != null)
            heroInfo.skeletonAnimation.state.SetAnimation(0, "H_23101_Skill_1", false);//��ų
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);

        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());

            skillData.Effect(heroInfo, targetInfo);

            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    public override void Detect()
    {
        for (int i = 0; i < soldierList.Count; i++)//Awake���� ���� ���߿� ���� SoldierList ���ε��� �������ֱ�, ��Ʋ ���� ���� ���� �ֱ�?
        {
            if (soldierList[i] != heroInfo && !(soldierList[i].debuffCoroutine.ContainsKey(((SingleDebuffData)skillData).debuffCode) && soldierList[i].debuffCoroutine[((SingleDebuffData)skillData).debuffCode].Count < ((SingleDebuffData)skillData).max_Stack))
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
