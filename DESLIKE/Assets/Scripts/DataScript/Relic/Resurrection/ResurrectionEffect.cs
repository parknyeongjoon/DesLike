using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResurrectionEffect : MonoBehaviour
{
    HeroInfo heroInfo;

    void Awake()
    {
        heroInfo = GetComponentInParent<HeroInfo>();
        heroInfo.beforeDeadEvent.AddListener(Effect);
        heroInfo.resurrection = true;
    }

    void Effect()
    {
        heroInfo.cur_Hp = heroInfo.castleData.hp * 0.15f;
        Debug.Log("resurrection");
        heroInfo.resurrection = false;
        heroInfo.beforeDeadEvent.RemoveListener(Effect);
    }
}
