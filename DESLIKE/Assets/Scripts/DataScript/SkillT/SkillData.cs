﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public override Effect로 각 스킬 효과 넣어주기
//각 effect에 extraSkillData.effect 넣어줘야 추가 효과 발동됨
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillT/SkillData")]
public class SkillData : ScriptableObject
{
    public string code, skill_name, toolTip;
    public SkillType skillType;
    public Sprite skill_Icon;
    public SkillData extraSkillData;

    public virtual void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        extraSkillData.Effect(heroInfo, targetInfo);
    }
}