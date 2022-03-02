﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class SoldierBehaviour : SoldierBasic//detect 함수 손보기
{
    public Func<HeroInfo, IEnumerator> skillHandler;
    public Action skillDetect;
    public Func<bool> canSkill;

    new void Start()
    {
        base.Start();
        heroInfo.healWeight = 1;
    }

    void FixedUpdate()
    {
        if (heroInfo.target)
        {
            heroInfo.moveDir = (heroInfo.target.transform.position - transform.position).normalized;
        }
        else { heroInfo.moveDir = new Vector3(0, 0, 0); }
    }

    protected override IEnumerator Idle_Behaviour()
    {
        while(heroInfo.state == Soldier_State.Idle)
        {
            heroInfo.state = Soldier_State.Detect;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Detect_Behaviour());
    }

    protected override IEnumerator Detect_Behaviour()
    {
        Coroutine detectCoroutine = StartCoroutine(Detect());
        heroInfo.animator.SetBool("isWalk", true);
        while (heroInfo.state == Soldier_State.Detect)
        {
            Move();
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(detectCoroutine);
        if(heroInfo.state == Soldier_State.Siege) { StartCoroutine(Siege_Behaviour()); }
        else if(heroInfo.state == Soldier_State.Battle) { StartCoroutine(Battle_Behaviour()); }
    }

    protected IEnumerator Detect()
    {
        while (true)
        {
            atkDetect?.Invoke();
            skillDetect?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override IEnumerator Siege_Behaviour()
    {
        Coroutine detectCoroutine = StartCoroutine(Detect());
        heroInfo.animator.SetBool("isWalk", false);
        while (heroInfo.state == Soldier_State.Siege)
        {
            if(canAtk != null && !canAtk.Invoke())
            {
                yield return StartCoroutine(atkHandler?.Invoke(heroInfo.targetInfo));
            }
            else
            {
                Move();
            }
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(detectCoroutine);
        if(heroInfo.state == Soldier_State.Battle) { StartCoroutine(Battle_Behaviour()); }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        heroInfo.animator.SetBool("isWalk", false);
        while (heroInfo.state == Soldier_State.Battle)
        {
            if (canSkill != null && canSkill.Invoke())
            {
                yield return StartCoroutine(skillHandler?.Invoke(heroInfo.skillTargetInfo as HeroInfo));
            }
            else if (canAtk != null && canAtk.Invoke())
            {
                yield return StartCoroutine(atkHandler?.Invoke(heroInfo.targetInfo));
            }
            else if(!heroInfo.targetInfo)//skillTargetInfo도 넣어야하나?
            {
                heroInfo.state = Soldier_State.Idle;
            }
            else
            {
                Move();
            }
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle_Behaviour());
    }

    protected override IEnumerator Stun_Behaviour()
    {
        while (heroInfo.state == Soldier_State.Stun)
        {
            Debug.Log("스턴");
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle_Behaviour());
    }
}
