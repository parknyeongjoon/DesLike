using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroPanel : MonoBehaviour
{
    [SerializeField]
    Image Hero_Portrait, HPBar, MPBar;
    [SerializeField]
    Image[] SkillIcons, BlackSkillIcons;
    [SerializeField]
    TMP_Text HPText, MPText;
    [SerializeField]
    TMP_Text[] SkillCooltimes;
    [SerializeField]
    GameObject buffPanel;
    [SerializeField]
    GameObject buffObject, debuffObject;
    [SerializeField]
    Image buffImg, debuffImg;

    Dictionary<string, GameObject> buffDic = new Dictionary<string, GameObject>();

    GameObject hero;
    HeroInfo heroInfo;
    Skill[] skills = new Skill[3];

    void Awake()
    {
        hero = GameObject.Find("Hero");
        heroInfo = hero.GetComponent<HeroInfo>();
        skills = hero.GetComponent<HeroSkillUse>().skillScripts;
        buffImg = buffObject.GetComponentInChildren<Image>();
        debuffImg = debuffObject.GetComponentInChildren<Image>();
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
        foreach(string code in heroInfo.buffCoroutine.Keys)
        {
            buffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(buffObject, buffPanel.transform));
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.buffCoroutine[code].Count.ToString();
        }
        foreach(string code in heroInfo.debuffCoroutine.Keys)
        {
            debuffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(debuffObject, buffPanel.transform));
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.debuffCoroutine[code].Count.ToString();
        }
        //코루틴 시작
        StartCoroutine(RenewalHeroPanel());
        for(int i = 0; i < skills.Length; i++)
        {
            StartCoroutine(RenewalSkillPanel(i));
        }
    }

    public IEnumerator RenewalHeroPanel()//OnDamaged시에만으로 한정하기
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

    public IEnumerator RenewalSkillPanel(int index)//코루틴으로 3개 돌리기 스킬 쓰고 나면 코루틴 돌리는 식으로
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

    public void AddBuff(string code)//얘들도 오브젝트 풀링해도 될 듯
    {
        if (buffDic.ContainsKey(code))
        {
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.buffCoroutine[code].Count.ToString();
        }
        else
        {
            buffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(buffObject, buffPanel.transform));
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.buffCoroutine[code].Count.ToString();
        }
    }

    public void AddDebuff(string code)//얘들도 오브젝트 풀링해도 될 듯
    {
        if (buffDic.ContainsKey(code))
        {
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.debuffCoroutine[code].Count.ToString();
        }
        else
        {
            debuffImg.sprite = SaveManager.Instance.dataSheet.skillDataSheet[code].skill_Icon;
            buffDic.Add(code, Instantiate(debuffObject, buffPanel.transform));
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.debuffCoroutine[code].Count.ToString();
        }
    }

    public void RemoveBuff(string code)
    {
        if (heroInfo.buffCoroutine[code].Count == 0)
        {
            Destroy(buffDic[code]);
            buffDic.Remove(code);
        }
        else
        {
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.buffCoroutine[code].Count.ToString();
        }
    }

    public void RemoveDebuff(string code)
    {
        if (heroInfo.debuffCoroutine[code].Count == 0)
        {
            Destroy(buffDic[code]);
            buffDic.Remove(code);
        }
        else
        {
            buffDic[code].GetComponentInChildren<Text>().text = heroInfo.debuffCoroutine[code].Count.ToString();
        }
    }
}