using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    public HeroInfo heroInfo;
    public SkillData skillData;

    protected virtual void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
    }
}