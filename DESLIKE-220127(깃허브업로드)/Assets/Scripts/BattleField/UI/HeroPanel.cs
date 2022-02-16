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
    Skill[] skills = new Skill[3];

    void Awake()
    {
        hero = GameObject.Find("Hero");
        heroInfo = hero.GetComponent<HeroInfo>();
        skills = hero.GetComponent<HeroSkillUse>().skillScripts;

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
        heroInfo.castleData = SaveManager.Instance.gameData.heroSaveData.heroData;
        Hero_Portrait.sprite = heroInfo.castleData.sprite;
        for (int i = 0; i < skills.Length; i++)
        {
            //skillDatas[i] = heroData.skillList[i];
            SkillIcons[i].sprite = skills[i].skillData.skill_Icon;
            BlackSkillIcons[i].sprite = skills[i].skillData.skill_Icon;
        }
    }

    public void RenewalHeroPanel()
    {
        HPBar.fillAmount = heroInfo.cur_Hp / heroInfo.castleData.hp;
        HPText.text = heroInfo.cur_Hp + "/" + heroInfo.castleData.hp;
        MPBar.fillAmount = heroInfo.cur_Mp / ((HeroData)heroInfo.castleData).mp;
        MPText.text = heroInfo.cur_Mp + "/" + ((HeroData)heroInfo.castleData).mp;
    }

    public void RenewalSkillPanel()
    {
        //if(스킬이 액티브인 경우)
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] as ActiveSkill)
            {
                SkillIcons[i].fillAmount = 1 - ((ActiveSkill)skills[i]).cur_cooltime / ((ActiveSkillData)skills[i].skillData).cooltime;
                SkillCooltimes[i].text = (((ActiveSkill)skills[i]).cur_cooltime > 0 ? ((int)((ActiveSkill)skills[i]).cur_cooltime).ToString() : "");
            }
        }
    }
}