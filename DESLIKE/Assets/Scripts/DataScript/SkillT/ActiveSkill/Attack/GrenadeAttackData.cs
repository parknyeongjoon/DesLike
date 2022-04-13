using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrenadeAttackData", menuName = "ScriptableObject/SkillT/GrenadeAttackData")]
public class GrenadeAttackData : ActiveSkillData
{
    public int max_Target;
    public float extent, atk_Dmg;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        HeroInfo[] targetInfos;
        targetInfos = Get_Targets(heroInfo, targetInfo);
        if (targetInfos != null)
        {
            for (int i = 0; i < targetInfos.Length; i++)
            {
                targetInfos[i].OnDamaged(atk_Dmg);
            }
        }
    }

    HeroInfo[] Get_Targets(HeroInfo heroInfo, HeroInfo targetInfo)// 다른 곳으로 static으로 옮기기, soldier에서 스킬 사용시 null이면 마우스 위치에 사용될 듯?
    {
        HeroInfo[] targetInfos;
        Vector3 skillPos = new Vector3();
        if (targetInfo) { skillPos = targetInfo.transform.position; }
        else { skillPos = MouseManager.Instance.skillPos; }
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(skillPos, extent, ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
        if(targetColliders.Length == 0)
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
        else if(targetColliders.Length > max_Target)
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
