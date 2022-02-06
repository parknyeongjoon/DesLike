using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    protected HeroInfo heroInfo;
    protected SoldierBehaviour soldierBehaviour;
    public SkillData skillData;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
        soldierBehaviour = GetComponent<SoldierBehaviour>();
    }
}