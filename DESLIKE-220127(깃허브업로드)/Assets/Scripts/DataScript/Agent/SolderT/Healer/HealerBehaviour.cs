using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealerBehaviour : SoldierBasic
{
    new void Start()
    {
        base.Start();
        heroInfo.healWeight = -1;
    }

    void FixedUpdate()
    {
        if (heroInfo.skillTarget)
        {
            heroInfo.moveDir = (heroInfo.skillTarget.transform.position - new Vector3(2, 0, 0) - transform.position).normalized;
        }
        else { heroInfo.moveDir = new Vector3(0, 0, 0); }
    }

    protected override IEnumerator Idle_Behaviour()
    {
        heroInfo.action = Soldier_Action.Idle;
        while (heroInfo.state == Soldier_State.Idle)
        {
            heroInfo.state = Soldier_State.Detect;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Detect_Behaviour());
    }

    protected override IEnumerator Detect_Behaviour()
    {
        Coroutine detectCoroutine = StartCoroutine(Detect());
        if (heroInfo.action != Soldier_Action.Move) { StartCoroutine(Move()); }
        while (heroInfo.state == Soldier_State.Detect)
        {
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(detectCoroutine);
        if (heroInfo.state == Soldier_State.Battle) { StartCoroutine(Battle_Behaviour()); }
        else { StartCoroutine(Idle_Behaviour()); }
    }

    protected IEnumerator Detect()
    {
        while (true)
        {
            skillDetect?.Invoke();
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        while(heroInfo.state == Soldier_State.Battle)
        {
            if (canSkill != null && canSkill.Invoke())
            {
                yield return StartCoroutine(skillHandler?.Invoke(heroInfo.skillTargetInfo as HeroInfo));
                heroInfo.state = Soldier_State.Charge;
            }
            else
            {
                heroInfo.state = Soldier_State.Idle;
            }
            yield return new WaitForFixedUpdate();
        }
        if(heroInfo.state == Soldier_State.Idle) { StartCoroutine(Idle_Behaviour()); }
        else if(heroInfo.state == Soldier_State.Charge) { StartCoroutine(Charge_Behaviour()); }
    }

    protected override IEnumerator Charge_Behaviour()
    {
        while(heroInfo.state == Soldier_State.Charge)
        {
            if (heroInfo.action != Soldier_Action.Move) { StartCoroutine(Move(heroInfo.skillTarget.transform.position - new Vector3(2, 0, 0))); }
            if (isSkillActive.Invoke())
            {
                heroInfo.state = Soldier_State.Idle;
            }
            yield return new WaitForFixedUpdate();
        }
        if(heroInfo.state == Soldier_State.Idle) { StartCoroutine(Idle_Behaviour()); }
    }
}