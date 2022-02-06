using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackBehaviour : ActiveSkillBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    public void UseSkill(CastleInfo targetInfo)
    {
        StartCoroutine(((SingleAttackData)skillData).Effect(targetInfo, heroInfo));
        cur_cooltime = ((ActiveSkillData)skillData).cooltime;
        StartCoroutine(SkillCooltime());
    }
}
