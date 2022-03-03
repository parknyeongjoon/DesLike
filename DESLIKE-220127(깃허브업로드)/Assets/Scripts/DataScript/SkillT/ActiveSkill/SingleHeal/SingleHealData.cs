using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHealData : ActiveSkillData
{
    public GameObject healEffect;
    public float heal_Amount;

    public void Effect(HeroInfo targetInfo)
    {
        targetInfo.OnHealed(heal_Amount);
    }
}
