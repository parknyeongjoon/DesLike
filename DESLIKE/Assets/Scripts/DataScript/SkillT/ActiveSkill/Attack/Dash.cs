using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dash",menuName ="ScriptableObject/SkillT/Dash")]
public class Dash : GrenadeAttackData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        base.Effect(heroInfo, targetInfo);
        heroInfo.transform.position += new Vector3(extent, 0);
    }

    protected override HeroInfo[] Get_Targets(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        HeroInfo[] targetInfos;
        Vector3 skillPos;
        if (targetInfo) { skillPos = targetInfo.transform.position; }
        else { skillPos = MouseManager.Instance.skillPos; }
        Collider2D[] targetColliders = Physics2D.OverlapAreaAll(skillPos, skillPos + new Vector3(extent, 0), ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
        Debug.DrawLine(skillPos, skillPos + new Vector3(extent, 0));
        if (targetColliders.Length == 0)
        {
            return null;
        }
        else if (targetColliders.Length <= max_Target)
        {
            targetInfos = new HeroInfo[targetColliders.Length];
            for (int i = 0; i < targetColliders.Length; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<HeroInfo>();
            }
            return targetInfos;
        }
        else if (targetColliders.Length > max_Target)
        {
            targetInfos = new HeroInfo[max_Target];
            bool isTarget = false;
            for (int i = 0; i < max_Target - 1; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<HeroInfo>();
                if (targetInfos[i] == targetInfo) { isTarget = true; }
            }
            if (isTarget) { targetColliders[max_Target - 1].GetComponent<HeroInfo>(); }
            else { targetInfos[max_Target - 1] = targetInfo; }
            return targetInfos;
        }
        return null;
    }
}