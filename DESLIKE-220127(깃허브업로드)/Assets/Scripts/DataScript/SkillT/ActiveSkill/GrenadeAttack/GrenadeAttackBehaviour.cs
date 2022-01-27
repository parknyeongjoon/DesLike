using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttackBehaviour : ActiveSkillBehaviour
{
    public GrenadeAttackData grenadeAttackSkillData;
    public int atkArea, atkLayer;

    new void Start()
    {
        base.Start();
        atkArea = (int)heroInfo.team * (int)grenadeAttackSkillData.atkArea;
        atkLayer = (int)grenadeAttackSkillData.atkArea * 7;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Detect_Behaviour()
    {
        DoSkill();
    }

    protected override void Battle_Behaviour()
    {
        DoSkill();
    }
    //위로 영웅 능력 사용에는 필요없음
    void DoSkill()
    {
        if (CanSkillCheck())
        {
            Collider2D[] targetColliders = Physics2D.OverlapCircleAll(heroInfo.target.transform.position, grenadeAttackSkillData.extent, atkArea ^ atkLayer);
            CastleInfo[] targetInfos = new CastleInfo[grenadeAttackSkillData.max_Target];
            for (int i = 0; i < targetColliders.Length && i < grenadeAttackSkillData.max_Target; i++)
            {
                targetInfos[i] = targetColliders[i].GetComponent<CastleInfo>();
            }
                StartCoroutine(SkillCoroutine(targetInfos));
        }
    }

    public bool CanSkillCheck()
    {
        if (CheckDistance(grenadeAttackSkillData.range) && cur_cooltime <= 0 && heroInfo.cur_Mp >= grenadeAttackSkillData.mp)
        {
            return true;
        }
        return false;
    }

    public IEnumerator SkillCoroutine(CastleInfo[] targetInfos)
    {
        heroInfo.cur_Mp -= grenadeAttackSkillData.mp;
        grenadeAttackSkillData.Effect(targetInfos);
        cur_cooltime = grenadeAttackSkillData.cooltime;
        while (cur_cooltime >= 0)
        {
            cur_cooltime -= Time.deltaTime;
            yield return null;
        }
    }
}