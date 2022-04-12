using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillT/SkillData")]
public class SkillData : ScriptableObject
{
    public string code, skill_name;
    public SkillType skillType;
    public Sprite skill_Icon;

    public virtual void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {

    }
}