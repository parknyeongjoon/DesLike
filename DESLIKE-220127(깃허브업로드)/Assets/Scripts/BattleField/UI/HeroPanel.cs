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
    [SerializeField]
    Transform buffPanel;

    Dictionary<string, SkillData> buffDic;

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

    public void SetHeroPanel()
    {
        heroInfo.castleData = SaveManager.Instance.gameData.heroSaveData.heroData;
        Hero_Portrait.sprite = heroInfo.castleData.sprite;
        for (int i = 0; i < skills.Length; i++)
        {
            SkillIcons[i].sprite = skills[i].skillData.skill_Icon;
            BlackSkillIcons[i].sprite = skills[i].skillData.skill_Icon;
        }
        StartCoroutine(RenewalHeroPanel());
        for(int i = 0; i < skills.Length; i++)
        {
            StartCoroutine(RenewalSkillPanel(i));
        }
    }

    public IEnumerator RenewalHeroPanel()
    {
        while (true)
        {
            HPBar.fillAmount = heroInfo.cur_Hp / heroInfo.castleData.hp;
            HPText.text = heroInfo.cur_Hp + "/" + heroInfo.castleData.hp;
            MPBar.fillAmount = heroInfo.cur_Mp / ((HeroData)heroInfo.castleData).mp;
            MPText.text = heroInfo.cur_Mp + "/" + ((HeroData)heroInfo.castleData).mp;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator RenewalSkillPanel(int index)//코루틴으로 3개 돌리기
    {
        if (skills[index] as ActiveSkill)
        {
            while (true)
            {
                SkillIcons[index].fillAmount = 1 - ((ActiveSkill)skills[index]).cur_cooltime / ((ActiveSkillData)skills[index].skillData).cooltime;
                SkillCooltimes[index].text = (((ActiveSkill)skills[index]).cur_cooltime > 0 ? ((int)((ActiveSkill)skills[index]).cur_cooltime).ToString() : "");
                yield return new WaitForFixedUpdate();
            }
        }
    }
}