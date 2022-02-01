using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : MonoBehaviour
{
    [SerializeField]
    Image Hero_Portrait, HPBar, MPBar;
    [SerializeField]
    Image[] SkillIcons, BlackSkillIcons;
    [SerializeField]
    Text HPText, MPText;
    [SerializeField]
    Text[] SkillCooltimes;

    GameObject hero;
    HeroInfo heroInfo;
    HeroData heroData;

    SkillBehaviour[] skillBehaviours;
    SkillData[] skillDatas = new SkillData[4];

    void Awake()
    {
        hero = GameObject.Find("Hero");
        heroInfo = hero.GetComponent<HeroInfo>();
        skillBehaviours = hero.GetComponent<HeroSkillUse>().heroSkillList;

    }

    void Start()
    {
        SetHeroPanel();
    }

    void FixedUpdate()
    {
        RenewalHeroPanel();
        RenewalSkillPanel();
    }

    public void SetHeroPanel()
    {
        heroData = SaveManager.Instance.gameData.heroSaveData.heroData;
        Hero_Portrait.sprite = heroData.sprite;
        for (int i = 0; i < skillBehaviours.Length; i++)
        {
            skillDatas[i] = heroData.skillList[i];
            SkillIcons[i].sprite = skillDatas[i].skill_Icon;
            BlackSkillIcons[i].sprite = skillDatas[i].skill_Icon;
        }
    }

    public void RenewalHeroPanel()
    {
        HPBar.fillAmount = heroInfo.cur_Hp / heroData.hp;
        HPText.text = heroInfo.cur_Hp + "/" + heroData.hp;
        MPBar.fillAmount = heroInfo.cur_Mp / heroData.mp;
        MPText.text = heroInfo.cur_Mp + "/" + heroData.mp;
    }

    public void RenewalSkillPanel()
    {
        //if(스킬이 액티브인 경우)
        for (int i = 0; i < skillBehaviours.Length; i++)
        {
            if (skillBehaviours[i] is ActiveSkillBehaviour)
            {
                SkillIcons[i].fillAmount = 1 - ((ActiveSkillBehaviour)skillBehaviours[i]).cur_cooltime / ((ActiveSkillData)skillDatas[i]).cooltime;
                SkillCooltimes[i].text = (((ActiveSkillBehaviour)skillBehaviours[i]).cur_cooltime > 0 ? ((int)((ActiveSkillBehaviour)skillBehaviours[i]).cur_cooltime).ToString() : "");
            }
        }
    }
}