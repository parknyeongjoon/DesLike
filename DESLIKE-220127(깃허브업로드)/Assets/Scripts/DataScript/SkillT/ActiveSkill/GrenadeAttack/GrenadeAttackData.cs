using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrenadeAttackData", menuName = "ScriptableObject/SkillT/GrenadeAttackData")]
public class GrenadeAttackData : ActiveSkillData
{
    public int max_Target;
    public float extent, atk_Dmg;

    public IEnumerator Effect(CastleInfo targetInfo, HeroInfo heroInfo)
    {
        CastleInfo[] targetInfos;
        yield return new WaitForSeconds(start_Delay);
        if (targetInfo)
        {
            heroInfo.cur_Mp -= mp;
            targetInfos = Get_Targets(targetInfo, heroInfo);
            for (int i = 0; i < targetInfos.Length; i++)
            {
                targetInfos[i].OnDamaged(atk_Dmg);
            }
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    public CastleInfo[] Get_Targets(CastleInfo targetInfo, HeroInfo heroInfo)// 다른 곳으로 static으로 옮기기, soldier에서 스킬 사용시 null이면 마우스 위치에 사용될 듯?
    {
        CastleInfo[] targetInfos = new CastleInfo[max_Target];
        Vector3 skillPos = new Vector3();
        if (targetInfo) { skillPos = targetInfo.transform.position; }
        else { skillPos = Input.mousePosition; }
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(skillPos, extent, ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
        if (targetColliders.Length <= max_Target)
        {
            for (int i = 0; i < targetColliders.Length; i++)//왜 경고 뜨지?
            {
                targetInfos[i] = targetColliders[i].GetComponent<CastleInfo>();
                return targetInfos;
            }
        }
        else
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
