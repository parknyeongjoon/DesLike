 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierBasic : MonoBehaviour
{
    public HeroInfo heroInfo;

    public Func<CastleInfo, IEnumerator> atkHandler;
    public Action atkDetect;
    public Func<bool> canAtk;

    public Func<HeroInfo, IEnumerator> skillHandler;
    public Action skillDetect;
    public Func<bool> isSkillActive;
    public Func<bool> canSkill;

    protected Coroutine moveCoroutine;

    public const int basicSoundWeight = 10;//사운드 가중치 최소값
    public int curSoundWeight;//현재 사운드 가중치

    protected void Start()
    {
        heroInfo.moveDir = new Vector3(0, 0, 0);
        StartCoroutine(Idle_Behaviour());
    }

    protected virtual IEnumerator Idle_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Detect_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Battle_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Charge_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Stun_Behaviour(float stunTime)
    {
        while (heroInfo.state == Soldier_State.Stun)
        {
            Debug.Log("스턴");
            yield return new WaitForSeconds(stunTime);
        }
        heroInfo.state = Soldier_State.Idle;
        StartCoroutine(Idle_Behaviour());
    }

    protected virtual void Dead_Behaviour()
    {

    }

    protected void OnMouseEnter()//EnemyResource에만 따로넣기
    {
        if(MouseManager.Instance.mouseState == Mouse_State.Grenade && heroInfo.team == Team.Enemy && !Input.GetKey(KeyCode.LeftAlt) && gameObject.layer != 7)
        {
            MouseManager.Instance.SetGrenadeExtent(transform);
        }
    }

    protected IEnumerator Move()
    {
        //heroInfo.skeletonAnimation.state.AddAnimation(0, "move", true, 0);//idle
        heroInfo.action = Soldier_Action.Move;
        while (heroInfo.action == Soldier_Action.Move)
        {
            transform.Translate(Time.deltaTime * (((HeroData)heroInfo.castleData).speed + heroInfo.buff_Stat.speed) * heroInfo.moveDir);
            yield return new WaitForFixedUpdate();
        }
    }

    protected IEnumerator Move(Vector3 destination)
    {
        //heroInfo.skeletonAnimation.state.AddAnimation(0, "move", true, 0);//idle
        heroInfo.action = Soldier_Action.Move;
        while (transform.position != destination && heroInfo.action == Soldier_Action.Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * (((HeroData)heroInfo.castleData).speed + heroInfo.buff_Stat.speed));
            yield return new WaitForFixedUpdate();
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
