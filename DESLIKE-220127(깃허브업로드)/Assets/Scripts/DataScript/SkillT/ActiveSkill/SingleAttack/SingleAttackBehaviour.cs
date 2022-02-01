using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackBehaviour : ActiveSkillBehaviour
{
    public int atkArea, atkLayer;

    protected override void Start()
    {
        base.Start();
        atkArea = (int)heroInfo.team * (int)((ActiveSkillData)skillData).atkArea;
        atkLayer = (int)((ActiveSkillData)skillData).atkArea * 7;
    }

    public void UseSkill(CastleInfo targetInfo)
    {
        StartCoroutine(((SingleAttackData)skillData).Effect(targetInfo, heroInfo));
        cur_cooltime = ((ActiveSkillData)skillData).cooltime;
        StartCoroutine(SkillCooltime());
    }
}
