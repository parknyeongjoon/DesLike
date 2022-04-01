using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGrenadeAttack",menuName = "ScriptableObject/BasicAttack/BasicGrenadeAttack")]
public class BasicGrenadeAttackData : BasicSingleAttackData
{
    public int max_Target;
    public float extent;

    public void Effect(CastleInfo targetInfo, HeroInfo heroInfo)
    {
        CastleInfo[] targetInfos;
        targetInfos = Get_Targets(targetInfo, heroInfo);
        for (int i = 0; i < targetInfos.Length; i++)
        {
            targetInfos[i].OnDamaged(atk_Dmg);
        }
    }

    CastleInfo[] Get_Targets(CastleInfo targetInfo, HeroInfo heroInfo)// 다른 곳으로 static으로 옮기기, soldier에서 스킬 사용시 null이면 마우스 위치에 사용될 듯?
    {
        CastleInfo[] targetInfos = new CastleInfo[max_Target];
        Vector3 skillPos = new Vector3();
        if (targetInfo) { skillPos = targetInfo.transform.position; }
        else { skillPos = Input.mousePosition; }
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(skillPos, extent, ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
        if(targetColliders.Length == 0)
        {
            return null;
        }
        else if (targetColliders.Length <= max_Target)
        {
            for (int i = 0; i < targetColliders.Length; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<CastleInfo>();
            }
            return targetInfos;
        }
        else if(targetColliders.Length > max_Target)
        {
            bool isTarget = false;
            for (int i = 0; i < max_Target - 1; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<CastleInfo>();
                if (targetInfos[i] == targetInfo) { isTarget = true; }
            }
            if (isTarget) { targetColliders[max_Target - 1].GetComponent<CastleInfo>(); }
            else { targetInfos[max_Target - 1] = targetInfo; }
            return targetInfos;
        }
        return null;
    }
}