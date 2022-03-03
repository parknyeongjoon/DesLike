using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerBehaviour : SoldierBasic
{
    new void Start()
    {
        base.Start();
        heroInfo.healWeight = -100;
    }

    protected override IEnumerator Idle_Behaviour()
    {
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
        if (heroInfo.action != Soldier_Action.Move) { StartCoroutine(Move(heroInfo.skillTarget.transform.position - new Vector3(2, 0, 0))); }
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
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        while(heroInfo.state == Soldier_State.Battle)
        {
            if (canSkill != null && canSkill.Invoke())
            {
                yield return StartCoroutine(skillHandler?.Invoke(heroInfo.skillTargetInfo as HeroInfo));
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
            yield return new WaitForFixedUpdate();
        }
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
    IEnumerator Heal()//탈출 타이밍 만들기 마나 0이면 날뜀
    {
        soldierInfo.State = State.Heal;
        while (TargetCheck(soldier.Range) && soldierInfo.Cur_mp >= ((HealerData)soldier).HealMp)//여기 조건부로 넣으면 될 듯
        {
            soldierInfo.Cur_mp -= ((HealerData)soldier).HealMp;
            soldierInfo.OnHealed(soldierInfo.SoldierData.Atk);
            StartCoroutine(Healing());
            yield return new WaitForSeconds(soldierInfo.SoldierData.Atk_Speed);
        }
        Set_Idle();
    }
    */
    /*
    IEnumerator Healing()
    {
        GameObject createBlood;
        createBlood = Instantiate(healEffect, heroInfo.target.transform);
        yield return new WaitForSeconds(0.8f);
        Destroy(createBlood);
    }
    */
}