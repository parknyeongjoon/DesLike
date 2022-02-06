using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillBehaviour : SkillBehaviour
{
    public float cur_cooltime;
    public int atkArea, atkLayer;

    protected override void Start()
    {
        base.Start();
        atkArea = (int)heroInfo.team * (int)((ActiveSkillData)skillData).atkArea;
        atkLayer = (int)((ActiveSkillData)skillData).atkArea * 7;
        cur_cooltime = 0;

        soldierBehaviour.skillDetect += Detect;
        soldierBehaviour.canSkill += CanSkillCheck;
    }

    public bool CanSkillCheck()
    {
        if (cur_cooltime <= 0 && heroInfo.cur_Mp >= ((ActiveSkillData)skillData).mp && heroInfo.TargetCheck(((ActiveSkillData)skillData).range))
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

    protected void Detect()
    {
        Debug.Log("스킬 탐색");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
        if (targets != null)
        {
            heroInfo.target = heroInfo.FindNearestSoldier(targets);
            if (CanSkillCheck())
            {
                if (!(heroInfo.target.tag == "Castle")) { heroInfo.state = Soldier_State.Battle; }
                heroInfo.targetInfo = heroInfo.target.GetComponent<CastleInfo>();
            }
        }
    }
}