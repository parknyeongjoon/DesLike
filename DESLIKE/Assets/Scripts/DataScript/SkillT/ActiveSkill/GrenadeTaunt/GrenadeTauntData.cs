using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrenadeTauntData", menuName = "ScriptableObject/SkillT/GrenadeTauntData")]
public class GrenadeTauntData : ActiveSkillData
{
    public int max_Target;
    public float extent, atk_Dmg;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//heroInfo�� ���� ���� ���� ���� ����
    {
        HeroInfo[] targetInfos;
        targetInfos = Get_Targets(heroInfo);
        if (targetInfos != null)
        {
            for (int i = 0; i < targetInfos.Length; i++)
            {
                targetInfos[i].state = Soldier_State.Taunt;
                targetInfos[i].target = heroInfo.gameObject;
                targetInfos[i].targetInfo = heroInfo;
                targetInfos[i].skillTarget = heroInfo.gameObject;
                targetInfos[i].skillTargetInfo = heroInfo;
            }
        }
    }

    HeroInfo[] Get_Targets(HeroInfo heroInfo)// �ٸ� ������ static���� �ű��, soldier���� ��ų ���� null�̸� ���콺 ��ġ�� ���� ��?
    {
        HeroInfo[] targetInfos;
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(heroInfo.transform.position, extent, ((int)atkArea * (int)heroInfo.team) ^ ((int)atkArea * 7));
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
            for(int i = 0; i < max_Target; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<HeroInfo>();
            }
            return targetInfos;
        }
        return null;
    }
}
