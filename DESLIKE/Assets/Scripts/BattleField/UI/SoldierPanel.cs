using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
//스킬 추가

public class SoldierPanel : MonoBehaviour
{
    [SerializeField]
    Image Soldier_Portrait, HPBar, MPBar, SkillIcon, BlackSkillIcon;
    [SerializeField]
    TMP_Text HPText, MPText, SkillCooltime;
    [SerializeField]
    GameObject buffPanel;
    [SerializeField]
    GameObject buffObject, debuffObject;
    [SerializeField]
    Image buffImg, debuffImg;

    SoldierInfo soldierInfo;
    SoldierData soldierData;

    Skill skill;
    SkillData skillData;

    Dictionary<string, GameObject> buffDic;

    void Awake()
    {
        buffImg = buffObject.GetComponentInChildren<Image>();
        debuffImg = debuffObject.GetComponentInChildren<Image>();
    }

    void OnEnable()
    {
        buffDic = new Dictionary<string, GameObject>();
        SetSoldierPanel();
    }

    void FixedUpdate()
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
        GameManager.DeleteChilds(buffPanel);
        foreach (string code in soldierInfo.buffCoroutine.Keys)
        {
            if(soldierInfo.buffCoroutine[code].Count != 0)
            {
                buffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
                buffDic.Add(code, Instantiate(buffObject, buffPanel.transform));
                buffDic[code].GetComponentInChildren<Text>().text = soldierInfo.buffCoroutine[code].Count.ToString();
            }
        }
        foreach (string code in soldierInfo.debuffCoroutine.Keys)
        {
            debuffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(debuffObject, buffPanel.transform));
            buffDic[code].GetComponentInChildren<Text>().text = soldierInfo.debuffCoroutine[code].Count.ToString();
        }
        RenewalSoldierPanel();
        RenewalSkillPanel();
    }

    void RenewalSoldierPanel()//피나 마나의 변경이 생길때만 바꾸기?
    {
        HPBar.fillAmount = soldierInfo.cur_Hp / soldierData.hp;
        HPText.text = soldierInfo.cur_Hp + "/" + soldierData.hp;
        MPBar.fillAmount = soldierInfo.cur_Mp / soldierData.mp;
        MPText.text = soldierInfo.cur_Mp + "/" + soldierData.mp;
    }

    void RenewalSkillPanel()//코루틴으로
    {
        if (skill as ActiveSkill)
        {
            SkillIcon.fillAmount = 1 - ((ActiveSkill)skill).cur_cooltime / ((ActiveSkillData)skillData).cooltime;
            SkillCooltime.text = (((ActiveSkill)skill).cur_cooltime > 0 ? ((int)((ActiveSkill)skill).cur_cooltime).ToString() : "");
        }
        else
        {
            SkillIcon.fillAmount = 1;
            SkillCooltime.text = "";
        }
    }

    public void AddBuff(string code)//얘들도 오브젝트 풀링해도 될 듯
    {
        if (!buffDic.ContainsKey(code))
        {
            buffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(buffObject, buffPanel.transform));
        }
        buffDic[code].GetComponentInChildren<Text>().text = (soldierInfo.buffCoroutine[code].Count + 1).ToString();
    }

    public void AddDebuff(string code)//얘들도 오브젝트 풀링해도 될 듯
    {
        if (!buffDic.ContainsKey(code))
        {
            debuffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(debuffObject, buffPanel.transform));
        }
        buffDic[code].GetComponentInChildren<Text>().text = (soldierInfo.debuffCoroutine[code].Count + 1).ToString();
    }

    public void RemoveBuff(string code)
    {
        if (soldierInfo.buffCoroutine[code].Count <= 1)
        {
            Destroy(buffDic[code]);
            buffDic.Remove(code);
        }
        else
        {
            buffDic[code].GetComponentInChildren<Text>().text = (soldierInfo.buffCoroutine[code].Count - 1).ToString();
        }
    }

    public void RemoveDebuff(string code)
    {
        if (soldierInfo.debuffCoroutine[code].Count <= 1)
        {
            Destroy(buffDic[code]);
            buffDic.Remove(code);
        }
        else
        {
            buffDic[code].GetComponentInChildren<Text>().text = (soldierInfo.debuffCoroutine[code].Count - 1).ToString();
        }
    }
}