using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleAttackData",menuName = "ScriptableObject/SkillT/SingleAttackData")]
public class SingleAttackData : ActiveSkillData
{
    public float atk_Dmg;

    public IEnumerator Effect(CastleInfo targetInfo, HeroInfo heroInfo)
    {
        yield return new WaitForSeconds(start_Delay);
        if (targetInfo)
        {
            heroInfo.cur_Mp -= mp;
            targetInfo.OnDamaged(atk_Dmg);
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
