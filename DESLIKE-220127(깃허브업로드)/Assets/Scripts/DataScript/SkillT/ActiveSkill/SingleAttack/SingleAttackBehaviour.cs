using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackBehaviour : ActiveSkillBehaviour
{
    public SingleAttackData singleAttackSkillData;

    new void Start()
    {
        base.Start();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Detect_Behaviour()
    {
        DoSkill();
    }

    protected override void Battle_Behaviour()
    {
        DoSkill();
    }
    //위로 영웅 능력 사용에는 필요없음(밑에 함수들을 skillData에 넣고 영웅 스킬에는 skillData만 넣기)
    void DoSkill()
    {
        if (CanSkillCheck())
        {
            StartCoroutine(SkillCoroutine(heroInfo.targetInfo));
        }
    }

    public bool CanSkillCheck()
    {
        if (CheckDistance(singleAttackSkillData.range) && cur_cooltime <= 0 && heroInfo.cur_Mp >= singleAttackSkillData.mp)
        {
            return true;
        }
        return false;
    }

    public IEnumerator SkillCoroutine(CastleInfo targetInfo)
    {
        heroInfo.cur_Mp -= singleAttackSkillData.mp;
        singleAttackSkillData.Effect(targetInfo);
        cur_cooltime = singleAttackSkillData.cooltime;
        while (cur_cooltime >= 0)
        {
            cur_cooltime -= Time.deltaTime;
            yield return null;
        }
    }
}
