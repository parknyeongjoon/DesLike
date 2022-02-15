using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//스킬 추가

public class SoldierPanel : MonoBehaviour
{
    [SerializeField]
    Image Soldier_Portrait, HPBar, MPBar, SkillIcon, BlackSkillIcon;
    [SerializeField]
    Text HPText, MPText, SkillCooltime;

    SoldierInfo soldierInfo;
    SoldierData soldierData;

    Skill skill;
    SkillData skillData;

    public void OnEnable()
    {
        SetSoldierPanel();
    }

    public void FixedUpdate()
    {
        RenewalSoldierPanel();
        RenewalSkillPanel();
    }

    void SetSoldierPanel()
    {
        //병사 정보
        soldierInfo = BattleUIManager.Instance.cur_Soldier;
        soldierData = (SoldierData)soldierInfo.castleData;
        Soldier_Portrait.sprite = soldierData.sprite;
        //스킬 정보
        skill = soldierInfo.gameObject.GetComponent<Skill>();
        if (skill != null)
        {
            skillData = skill.skillData;
            SkillIcon.sprite = skillData.skill_Icon;
            BlackSkillIcon.sprite = skillData.skill_Icon;
        }
        else
        {
            skillData = null;
            SkillIcon.sprite = null;
            BlackSkillIcon.sprite = null;
        }
    }

    void RenewalSoldierPanel()//피나 마나의 변경이 생길때만 바꾸기?
    {
        HPBar.fillAmount = soldierInfo.cur_Hp / soldierData.hp;
        HPText.text = soldierInfo.cur_Hp + "/" + soldierData.hp;
        MPBar.fillAmount = soldierInfo.cur_Mp / soldierData.mp;
        MPText.text = soldierInfo.cur_Mp + "/" + soldierData.mp;
    }

    void RenewalSkillPanel()
    {
        if (skill as ActiveSkill)
        {
            SkillIcon.fillAmount = 1 - ((ActiveSkill)skill).cur_cooltime / ((ActiveSkillData)skillData).cooltime;
            SkillCooltime.text = (((ActiveSkill)skill).cur_cooltime > 0 ? ((int)((ActiveSkill)skill).cur_cooltime).ToString() : "");
        }
    }
}