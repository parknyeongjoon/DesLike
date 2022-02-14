using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoldierSpriteManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    HeroInfo heroInfo;
    HeroData heroData;

    [SerializeField]
    GameObject Hpbar, Mpbar;
    [SerializeField]
    Image hpbar, mpbar;
    [SerializeField]
    GameObject hp_mp_bar;

    int sightNum;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => heroData == null);
        heroData = (HeroData)heroInfo.castleData;
        spriteRenderer.sprite = heroData.sprite;
        if (heroInfo.team == Team.Enemy)
        {
            spriteRenderer.flipX = true;
        }
        OneBoxScale();
        StartCoroutine(SetHpMpBar());
    }

    IEnumerator SetHpMpBar()
    {
        while (true)
        {
            hp_mp_bar.transform.position = transform.position + new Vector3(0, 1f, 0);
            hpbar.fillAmount = heroInfo.cur_Hp / heroData.hp;
            mpbar.fillAmount = heroInfo.cur_Mp / heroData.mp;
            yield return null;
        }
    }

    void OneBoxScale()
    {
        float hpscalex = 500f / heroData.hp;
        foreach (Transform child in Hpbar.transform)
        {
            child.gameObject.transform.localScale = new Vector3(hpscalex, 1, 1);
        }
        try
        {
            float mpscalex = 250f / heroData.mp;
            foreach (Transform child in Mpbar.transform)
            {
                child.gameObject.transform.localScale = new Vector3(mpscalex, 1, 1);
            }
        }
        catch (DivideByZeroException)
        {
            Mpbar.SetActive(false);
        }
    }
}
