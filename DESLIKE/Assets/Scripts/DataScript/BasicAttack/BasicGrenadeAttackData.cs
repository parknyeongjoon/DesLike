using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGrenadeAttack",menuName = "ScriptableObject/BasicAttack/BasicGrenadeAttack")]
public class BasicGrenadeAttackData : BasicSingleAttackData
{
    public int max_Target;
    public float extent;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        List<HeroInfo> targetInfos;
        targetInfos = Get_Targets(heroInfo, targetInfo);
        for (int i = 0; i < targetInfos.Count; i++)
        {
            targetInfos[i].OnDamaged(atk_Dmg);
            extraSkillData?.Effect(heroInfo, targetInfo);
        }
    }

    List<HeroInfo> Get_Targets(HeroInfo heroInfo, HeroInfo targetInfo)// 다른 곳으로 static으로 옮기기, soldier에서 스킬 사용시 null이면 마우스 위치에 사용될 듯?
    {
        List<HeroInfo> targetInfos = new List<HeroInfo>();
        Vector3 skillPos = new Vector3();
        if (targetInfo) { skillPos = targetInfo.transform.position; }//targetInfo가 있다면 targetInfo쪽에
        else { skillPos = Input.mousePosition; }//없다면 마우스 위치에 시전
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(skillPos, extent, ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
        if(targetColliders.Length == 0)
        {
            return targetInfos;
        }
        else if (targetColliders.Length <= max_Target)
        {
            for (int i = 0; i < targetColliders.Length; i++)
            {
                targetInfos.Add(targetColliders[i].GetComponent<HeroInfo>());
            }
            return targetInfos;
        }
        else if(targetColliders.Length > max_Target)
        {
            bool isTarget = false;
            for (int i = 0; i < max_Target - 1; i++)
            {
                targetInfos.Add(targetColliders[i].GetComponent<HeroInfo>());
                if (targetInfos[i] == targetInfo) { isTarget = true; }
            }
            if (isTarget) { targetInfos.Add(targetColliders[max_Target - 1].GetComponent<HeroInfo>()); }
            else { targetInfos.Add(targetInfo); }
            return targetInfos;
        }
        return null;
    }
}