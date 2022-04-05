using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class SoldierSpriteManager : MonoBehaviour
{
    [SerializeField]
    SkeletonAnimation skeletonAnimation;
    [SerializeField]
    AnimationReferenceAsset[] animClips;
    [SerializeField]
    HeroInfo heroInfo;
    HeroData heroData;

    [SerializeField]
    GameObject Hpbar, Mpbar;
    [SerializeField]
    Image hpbar, mpbar;
    [SerializeField]
    GameObject hp_mp_bar;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => heroData == null);
        heroData = (HeroData)heroInfo.castleData;
        if (heroInfo.team == Team.Ally)
        {
            skeletonAnimation.skeleton.FlipX = true;
        }
        SetHpMpBar();
        skeletonAnimation.state.SetAnimation(0, animClips[(int)AnimState.Move], true);//지우고 유닛 상태 바꿔줄 때 넣어주기
        OneBoxScale();
    }

    public void SetHpMpBar()//데미지 받을 때만 실행하기
    {
        if(heroInfo.state != Soldier_State.Dead)
        {
            hp_mp_bar.transform.position = transform.position + new Vector3(0, 1f, 0);
            hpbar.fillAmount = heroInfo.cur_Hp / heroData.hp;
            mpbar.fillAmount = heroInfo.cur_Mp / heroData.mp;
        }
    }

    void OneBoxScale()//지우고 그냥 bar형태로
    {
        float hpscalex = 500f / heroData.hp;
        foreach (Transform child in Hpbar.transform)
        {
            child.gameObject.transform.localScale = new Vector3(hpscalex, 1, 1);
        }
        if (heroData.mp == 0)
        {
            mpbar.gameObject.SetActive(false);
        }
        else
        {
            float mpscalex = 250f / heroData.mp;
            foreach (Transform child in Mpbar.transform)
            {
                child.gameObject.transform.localScale = new Vector3(mpscalex, 1, 1);
            }
        }
    }

    public void Dead()
    {
        hp_mp_bar.SetActive(false);
        skeletonAnimation.skeleton.SetColor(skeletonAnimation.skeleton.GetColor() - new Color(0, 0, 0, 0.3f));
    }
}
