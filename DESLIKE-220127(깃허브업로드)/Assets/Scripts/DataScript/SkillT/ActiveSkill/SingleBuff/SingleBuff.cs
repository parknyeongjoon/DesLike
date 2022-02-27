using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBuff : ActiveSkill
{
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
            if (targetInfo.buffCoroutine.ContainsKey(skillData.code) && !((SingleBuffData)skillData).isStack)//������ �����ϴٸ� dictionary�� Ű �ߺ� ���� �Ͼ��
            {
                StopCoroutine(heroInfo.buffCoroutine[skillData.code]);
                ((SingleBuffData)skillData).Remove_Buff(targetInfo);
            }
            targetInfo.buffCoroutine.Add(skillData.code, StartCoroutine(BuffCoroutine(targetInfo)));//���� �������ְ� heroInfo ���� ��ųʸ��� �־��ֱ�
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator BuffCoroutine(HeroInfo targetInfo)
    {
        ((SingleBuffData)skillData).Effect(targetInfo);
        yield return new WaitForSeconds(((SingleBuffData)skillData).buff_Time);
        ((SingleBuffData)skillData).Remove_Buff(targetInfo);
    }

    public override void Detect()
    {
        if (skillData.skillType == SkillType.InstanceSkill)//instance ��ų �������� �־��� ��� �����ϱ�
        {
            heroInfo.skillTarget = gameObject;
            heroInfo.skillTargetInfo = heroInfo;
            if (CanSkillCheck()) { heroInfo.state = Soldier_State.Battle; }
            return;
        }
        base.Detect();
    }
}
