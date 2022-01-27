using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillT/SkillData")]
public class SkillData : ScriptableObject
{
    public string code, sort, skill_name;//sort enum으로 바꾸기
    public Atk_Type atk_Type;
    public Sprite skill_Icon;

    public virtual void Effect(CastleInfo targetInfo)
    {
        Debug.Log("설정해야함");
    }

    public virtual void Effect(CastleInfo[] targetInfos)
    {
        Debug.Log("설정해야함");
    }
}