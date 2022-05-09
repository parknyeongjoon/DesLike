using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStack : Skill
{
    BasicAttackScript basicAttackScript;

    int stack;

    void Start()
    {
        basicAttackScript = GetComponent<BasicAttackScript>();
        basicAttackScript.afterAtkEvent += AfterAtkEffect;
    }

    void AfterAtkEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        stack++;
        if((stack % ((StackData)skillData).activeStack) == 0)
        {
            basicAttackScript.atkCount += 1;
        }
        else if(stack != 1 && (stack % ((StackData)skillData).activeStack) == 1)
        {
            basicAttackScript.atkCount -= 1;
        }
        Debug.Log("stack : " + stack);
        Debug.Log("actkCount : " + basicAttackScript.atkCount);
    }
}
