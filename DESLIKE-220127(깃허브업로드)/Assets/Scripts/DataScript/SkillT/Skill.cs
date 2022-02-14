using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    protected HeroInfo heroInfo;
    [SerializeField]
    protected SoldierBehaviour soldierBehaviour;
    public SkillData skillData;

    protected virtual void Start()
    {

    }
}