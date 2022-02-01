using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttackBehaviour : ActiveSkillBehaviour
{
    GrenadeAttackData grenadeAttackSkillData;
    public int atkArea, atkLayer;

    protected override void Start()
    {
        base.Start();
        grenadeAttackSkillData = (GrenadeAttackData)skillData;
        atkArea = (int)heroInfo.team * (int)grenadeAttackSkillData.atkArea;
        atkLayer = (int)grenadeAttackSkillData.atkArea * 7;
    }

    public void UseSkill(CastleInfo targetInfo)
    {
        StartCoroutine(((GrenadeAttackData)skillData).Effect(targetInfo, heroInfo));
        cur_cooltime = ((ActiveSkillData)skillData).cooltime;
        StartCoroutine(SkillCooltime());
    }
}