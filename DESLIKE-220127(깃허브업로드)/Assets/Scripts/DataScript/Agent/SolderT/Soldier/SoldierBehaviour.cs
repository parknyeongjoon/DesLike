using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class SoldierBehaviour : SoldierBasic//detect 함수 손보기
{
    //public Animator animator;

    protected SoldierData soldier;

    public delegate void AtkHandler(CastleInfo targetInfo);
    public AtkHandler atkHandler;
    public AtkHandler skillHandler;

    public delegate void DetectHandler();
    public DetectHandler atkDetect;
    public DetectHandler skillDetect;

    public delegate bool CanDoHandler();
    public CanDoHandler canAtk;
    public CanDoHandler canSkill;

    new void Start()
    {
        base.Start();
        soldier = (SoldierData)heroInfo.castleData;
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
            Debug.Log("Idle");
            heroInfo.state = Soldier_State.Detect;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Detect_Behaviour());
    }

    protected override IEnumerator Detect_Behaviour()
    {
        Coroutine detectCoroutine = StartCoroutine(Detect());
        while (heroInfo.state == Soldier_State.Detect)
        {
            Debug.Log("탐색");
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
        while (heroInfo.state == Soldier_State.Siege)
        {
            Debug.Log("공성");
            heroInfo.action = Soldier_Action.Attack;
            atkHandler?.Invoke(heroInfo.targetInfo);
            yield return new WaitUntil(() => heroInfo.action == Soldier_Action.Idle);
        }
        StopCoroutine(detectCoroutine);
        if(heroInfo.state == Soldier_State.Battle) { StartCoroutine(Battle_Behaviour()); }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        while (heroInfo.state == Soldier_State.Battle)
        {
            Debug.Log("전투");
            if (canSkill != null && canSkill.Invoke())
            {
                Debug.Log("스킬 사용");
                heroInfo.action = Soldier_Action.Skill;
                skillHandler?.Invoke(heroInfo.targetInfo);
                yield return new WaitUntil(() => heroInfo.action == Soldier_Action.Idle);
            }
            else if (canAtk != null && canAtk.Invoke())
            {
                Debug.Log("평타 사용");
                heroInfo.action = Soldier_Action.Attack;
                atkHandler?.Invoke(heroInfo.targetInfo);
                yield return new WaitUntil(() => heroInfo.action == Soldier_Action.Idle);
            }
            else
            {
                Debug.Log("둘 다 불가");
                heroInfo.state = Soldier_State.Idle;
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
    /*

    public virtual void Set_Idle()//override 클래스 : ranger
    {
        heroInfo.target = null;
        heroInfo.state = Soldier_State.Idle;
    }
    */
}
