 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBasic : MonoBehaviour
{
    public HeroInfo heroInfo;

    protected void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
    }

    protected virtual void FixedUpdate()
    {
        BehaviourControll();
    }

    protected void BehaviourControll()
    {
        switch (heroInfo.state)
        {
            case Soldier_State.Idle:
                Idle_Behaviour();
                break;
            case Soldier_State.Detect:
                Detect_Behaviour();
                break;
            case Soldier_State.Battle:
                Battle_Behaviour();
                break;
            case Soldier_State.Siege:
                Siege_Behaviour();
                break;
        }
    }

    protected virtual void Idle_Behaviour()
    {

    }

    protected virtual void Detect_Behaviour()
    {

    }

    protected virtual void Battle_Behaviour()
    {

    }

    protected virtual void Siege_Behaviour()
    {

    }
}
