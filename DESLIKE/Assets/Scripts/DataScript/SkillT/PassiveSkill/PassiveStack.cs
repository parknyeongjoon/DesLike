using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStack : Skill
{
    BasicAttack basicAttack;

    int stack;
    public int activeStack;

    void Start()
    {
        basicAttack = GetComponent<BasicAttack>();
        basicAttack.afterAtkEvent += AfterAtkEffect;
    }

    void AfterAtkEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        stack++;
        if(stack % activeStack == 0)
        {
            basicAttack.atkCount += 1;
        }
        else if(stack != 1 && stack % activeStack == 1)
        {
            basicAttack.atkCount -= 1;
        }
    }
}
