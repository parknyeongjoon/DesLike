using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    public float cur_cooltime;
    public int atkArea, atkLayer;

    protected override void Start()
    {
        base.Start();
        atkArea = (int)heroInfo.team * (int)((ActiveSkillData)skillData).atkArea;
        atkLayer = (int)((ActiveSkillData)skillData).atkArea * 7;
        cur_cooltime = 0;
    }

    public override bool CanSkillCheck()
    {
        if(!heroInfo.targetInfo || heroInfo.target.layer == 7)
        {
            heroInfo.state = Soldier_State.Idle;
            return false;
        }
        if (cur_cooltime <= 0 && heroInfo.cur_Mp >= ((ActiveSkillData)skillData).mp && heroInfo.TargetCheck(((ActiveSkillData)skillData).range + heroInfo.targetInfo.castleData.size))
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
        Debug.Log("스킬 탐색");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (heroInfo.TargetCheck(((ActiveSkillData)skillData).range + 2))
            {
                if (!(heroInfo.target.tag == "Castle")) { heroInfo.state = Soldier_State.Battle; }
                heroInfo.targetInfo = heroInfo.target.GetComponent<HeroInfo>();
            }
        }
    }

    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        yield return null;
    }
}