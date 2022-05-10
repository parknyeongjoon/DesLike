using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusPassiveSkill : Skill
{
    protected virtual void Start()
    {
        skillData.Effect(heroInfo, null);
    }
}
