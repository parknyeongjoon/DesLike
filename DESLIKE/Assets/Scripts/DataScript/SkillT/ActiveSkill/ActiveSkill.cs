using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    public float cur_cooltime;
    public int atkArea, atkLayer;

    protected override void Awake()
    {
        base.Awake();
        soldierBasic.skillDetect = Detect;
        soldierBasic.canSkill = CanSkillCheck;
        soldierBasic.skillHandler = UseSkill;
        soldierBasic.isSkillActive = IsActive;
        atkArea = (int)heroInfo.team * (int)((ActiveSkillData)skillData).atkArea;
        atkLayer = (int)((ActiveSkillData)skillData).atkArea * 7;
        cur_cooltime = 0;
    }

    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Skill;
        if (heroInfo.skeletonAnimation.skeleton != null)//지우기
            heroInfo.skeletonAnimation.state.SetAnimation(0, skillData.code, false);//스킬
        //AkSoundEngine.PostEvent(skillData.code, gameObject);활성화
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

    public bool IsActive()
    {
        if (cur_cooltime <= 0 && heroInfo.cur_Mp >= ((ActiveSkillData)skillData).mp)
        {
            return true;
        }
        return false;
    }

    public override bool CanSkillCheck()
    {
        if(!heroInfo.skillTargetInfo || heroInfo.skillTarget.layer == 7)
        {
            heroInfo.state = Soldier_State.Idle;
            return false;
        }
        if (IsActive() && heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + heroInfo.skillTargetInfo.castleData.size))
        {
            return true;
        }
        return false;
    }

    protected IEnumerator SkillCooltime()
    {
        while (cur_cooltime >= 0)
        {
            cur_cooltime -= Time.deltaTime;
            yield return null;
        }
    }

    public override void Detect()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.skillTarget = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(heroInfo.skillTarget, ((ActiveSkillData)skillData).range + 2))
            {
                heroInfo.state = Soldier_State.Battle;
                heroInfo.skillTargetInfo = heroInfo.skillTarget.GetComponent<HeroInfo>();
            }
        }
    }
}