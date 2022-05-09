using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStack : Skill
{
    BasicAttackScript basicAttackScript;

    int stack;
    public int activeStack;

    void Start()
    {
        basicAttackScript = GetComponent<BasicAttackScript>();
        basicAttackScript.afterAtkEvent += AfterAtkEffect;
    }

    void AfterAtkEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        stack++;
        if(stack % activeStack == 0)
        {
            basicAttackScript.atkCount += 1;
        }
        else if(stack != 1 && stack % activeStack == 1)
        {
            basicAttackScript.atkCount -= 1;
        }
    }
}
