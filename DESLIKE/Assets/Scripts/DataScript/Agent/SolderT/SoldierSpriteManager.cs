using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class SoldierSpriteManager : MonoBehaviour
{
    [SerializeField]
    HeroInfo heroInfo;
    HeroData heroData;

    [SerializeField]
    Image hpbar, mpbar;
    [SerializeField]
    GameObject hp_mp_bar;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => heroData == null);
        heroData = (HeroData)heroInfo.castleData;
        SetHpMpBar();
    }

    public void SetHpMpBar()//데미지 받을 때만 실행하기
    {
        if(heroInfo.state != Soldier_State.Dead)
        {
            hpbar.fillAmount = heroInfo.cur_Hp / heroData.hp;
            mpbar.fillAmount = heroInfo.cur_Mp / heroData.mp;
        }
    }

    public void Dead()
    {
        hp_mp_bar.SetActive(false);
    }
}
