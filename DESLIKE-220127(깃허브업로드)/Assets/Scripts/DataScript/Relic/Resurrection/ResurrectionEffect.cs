using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionEffect : MonoBehaviour
{
    HeroInfo heroInfo;

    void Awake()
    {
        heroInfo = GetComponentInParent<HeroInfo>();
        heroInfo.beforeDeadHandler += Effect;
        heroInfo.resurrection = true;
    }

    void Effect()
    {
        heroInfo.cur_Hp = heroInfo.castleData.hp * 0.15f;
        Debug.Log("resurrection");
        heroInfo.resurrection = false;
        heroInfo.beforeDeadHandler -= Effect;
    }
}
