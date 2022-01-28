using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//스킬 추가

public class SoldierPanel : MonoBehaviour
{
    [SerializeField]
    Image Soldier_Portrait, HPBar, MPBar, SkillIcon, BlackSkillIcon;
    [SerializeField]
    Text HPText, MPText, SkillCooltime;

    public SoldierInfo soldierInfo;
    SoldierData soldierData;

    public SkillBehaviour skillBehaviour;
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
        soldierInfo = BattleUIManager.Instance.cur_Soldier;
        soldierData = (SoldierData)soldierInfo.castleData;
        skillBehaviour = soldierInfo.transform.GetComponent<SkillBehaviour>();
        Soldier_Portrait.sprite = soldierData.sprite;
        if (skillData != null)
        {
            skillData = soldierData.skillList[0];
            SkillIcon.sprite = skillData.skill_Icon;
            BlackSkillIcon.sprite = skillData.skill_Icon;
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
        if (skillBehaviour as ActiveSkillBehaviour)
        {
            SkillIcon.fillAmount = 1 - ((ActiveSkillBehaviour)skillBehaviour).cur_cooltime / ((ActiveSkillData)skillData).cooltime;
            SkillCooltime.text = (((ActiveSkillBehaviour)skillBehaviour).cur_cooltime > 0 ? ((int)((ActiveSkillBehaviour)skillBehaviour).cur_cooltime).ToString() : "");
        }
    }
}