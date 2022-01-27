using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillBehaviour : SkillBehaviour
{
    public float cur_cooltime;

    protected new void Start()
    {
        base.Start();
        cur_cooltime = 0;
    }

    protected bool CheckDistance(float range)
    {
        if(heroInfo.target != null && Vector3.Distance(this.transform.position, heroInfo.target.transform.position) <= range)
        {
            return true;
        }
        return false;
    }
}
