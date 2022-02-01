using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillBehaviour : SkillBehaviour
{
    public float cur_cooltime;

    protected override void Start()
    {
        base.Start();
        cur_cooltime = 0;
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
}