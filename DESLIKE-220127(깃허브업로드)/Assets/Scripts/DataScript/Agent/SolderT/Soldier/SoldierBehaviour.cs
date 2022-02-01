using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class SoldierBehaviour : SoldierBasic//detect 함수 손보기
{
    //public Animator animator;

    protected SoldierData soldier;

    protected Vector3 targetPos;

    public int atkArea, atkLayer;
    public float atkRange;

    public delegate IEnumerator AtkHandler();
    public AtkHandler atkHandler;

    new void Start()
    {
        base.Start();
        soldier = (SoldierData)heroInfo.castleData;
        heroInfo.healWeight = 1;
        Set_Idle();
    }

    protected new void FixedUpdate()
    {
        heroInfo.moveDir = (targetPos - transform.position).normalized;
        base.FixedUpdate();
    }

    protected override void Idle_Behaviour()
    {
        Move();
        StartCoroutine(Detect());
    }

    protected override void Detect_Behaviour()//오버라이드 클래스 : ranger, healer
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
            if (heroInfo.target.CompareTag("Castle"))
            {
                heroInfo.state = Soldier_State.Siege;
            }
            else
            {
                heroInfo.state = Soldier_State.Battle;
            }
            StartCoroutine(atkHandler());
        }
    }

    protected override void Siege_Behaviour()
    {
        
    }

    protected override void Battle_Behaviour()
    {

    }

    protected virtual IEnumerator Detect()//override 클래스 : healer
    {
        //animator.SetBool("isWalk", true);
        heroInfo.state = Soldier_State.Detect;
        while (heroInfo.state != Soldier_State.Battle)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 100, atkArea ^ atkLayer);
            if (targets != null)
            {
                heroInfo.target = FindNearestSoldier(targets);
                heroInfo.targetInfo = heroInfo.target.GetComponent<CastleInfo>();//너무 낭비같음 한 번만 받기, 씬전환시에 터짐, 공격하기 전에 받기
                if(!heroInfo.target.CompareTag("Castle"))
                {
                    heroInfo.state = Soldier_State.Detect;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual void Set_Idle()//override 클래스 : ranger
    {
        heroInfo.target = null;
        heroInfo.state = Soldier_State.Idle;
    }

    protected void Move()
    {
        transform.Translate(Time.deltaTime * soldier.speed * heroInfo.moveDir);
    }

    protected GameObject FindNearestSoldier(Collider2D[] targets)
    {
        float minDistance = 10e9F;
        GameObject target = null;
        foreach(Collider2D collider in targets)
        {
            float distance = Vector3.Distance(this.transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = collider.gameObject;
            }
        }
        return target;
    }
}
