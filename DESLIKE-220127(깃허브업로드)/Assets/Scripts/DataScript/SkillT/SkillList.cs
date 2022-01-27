using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillList", menuName = "ScriptableObject/SkillT/SkillList")]
public class SkillList : ScriptableObject
{
    public List<SkillData> skillList;
}