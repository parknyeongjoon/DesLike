using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerBehaviour : SoldierBasic
{
    new void Start()
    {
        base.Start();
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
        heroInfo.animator.SetBool("isWalk", true);
        while (heroInfo.state == Soldier_State.Detect)
        {
            if(heroInfo.skillTarget != null)
            {
                Move(heroInfo.skillTarget.transform.position - new Vector3(2,0,0));
            }
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

        }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        yield return new WaitForFixedUpdate();
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
    GameObject healEffect;
    [SerializeField]
    PortDatas allyPortDatas;

    new void Start()
    {
        base.Start();
        soldier = (SoldierData)heroInfo.castleData;
        healEffect = ((HealerData)soldier).HealEffect;
        heroInfo.healWeight = -1;
        Set_Idle();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Detect_Behaviour()
    {
        Move();
        if (heroInfo.target == null)
        {
            Set_Idle();
        }
        else
        {
            targetPos = heroInfo.target.transform.position;
        }
        if (heroInfo.TargetCheck(atkRange))
        {
            //StartCoroutine(Heal());
        }
    }

    /*protected override IEnumerator Detect()//최적화 필요
    {
        while (heroInfo.state != Soldier_State.Heal)
        {
            heroInfo.targetInfo = allyPortDatas.spawnSoldierList[0];
            heroInfo.target = heroInfo.targetInfo.transform.gameObject;
            heroInfo.state = Soldier_State.Detect;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
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