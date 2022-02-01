﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillT/SkillData")]
public class SkillData : ScriptableObject
{
    public string code, sort, skill_name;
    public Atk_Type atk_Type;
    public Sprite skill_Icon;
}