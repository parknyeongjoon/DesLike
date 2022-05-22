 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierBasic : MonoBehaviour
{
    public HeroInfo heroInfo;

    public Func<HeroInfo, IEnumerator> atkHandler;
    public Action atkDetect;
    public Func<bool> canAtk;

    public Func<HeroInfo, IEnumerator> skillHandler;
    public Action skillDetect;
    public Func<bool> isSkillActive;
    public Func<bool> canSkill;

    public Coroutine moveCoroutine;

    public const int basicSoundWeight = 10;//사운드 가중치 최소값
    public int curSoundWeight;//현재 사운드 가중치

    float stunTime = 0;//스턴 지속시간, 새로운 스턴이 들어왔을 때 갱신용
    float tauntTime = 0;//도발 지속시간, 갱신용

    protected IEnumerator Start()
    {
        heroInfo.stunEvent.AddListener(Stun);
        heroInfo.moveDir = new Vector3(0, 0, 0);
        float waitTime = UnityEngine.Random.Range(0.0f, 2.0f);
        yield return new WaitUntil(() => BattleUIManager.Instance.battleStart == true);//배틀이 시작될 때까지 대기
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Idle_Behaviour());
    }

    public void Stun(float stunTime)
    {
        StartCoroutine(Stun_Behaviour(stunTime));
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

    public IEnumerator Stun_Behaviour(float _stunTime)//stunTime 밖으로 빼기
    {
        if(stunTime < _stunTime)
        {
            stunTime = _stunTime;
            StartCoroutine(StunTime());
        }
        while (heroInfo.state == Soldier_State.Stun)
        {
            Debug.Log("스턴");
            yield return new WaitForFixedUpdate();
        }
        heroInfo.skeletonAnimation.state.SetAnimation(0, heroInfo.castleData.code + "_Move", true);
        StartCoroutine(Idle_Behaviour());
    }

    public virtual IEnumerator Taunt_Behaviour(HeroInfo _targetInfo, float _tauntTime)//tauntTime 밖으로 빼기
    {
        if(tauntTime < _tauntTime)
        {
            tauntTime = _tauntTime;
            heroInfo.target = _targetInfo.gameObject;
            heroInfo.targetInfo = _targetInfo;
        }
        while(heroInfo.state == Soldier_State.Taunt)
        {
            Debug.Log("도발");
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle_Behaviour());
    }

    IEnumerator StunTime()
    {
        while(stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        heroInfo.state = Soldier_State.Idle;
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator TauntTime()
    {
        while(tauntTime > 0)
        {
            tauntTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        heroInfo.state = Soldier_State.Idle;
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
        if (heroInfo.skeletonAnimation.skeleton != null)
            heroInfo.skeletonAnimation.state.AddAnimation(0, heroInfo.castleData.code + "_Move", true, 0);//idle
        heroInfo.action = Soldier_Action.Move;
        while (heroInfo.action == Soldier_Action.Move)
        {
            transform.Translate(Time.deltaTime * (((HeroData)heroInfo.castleData).speed + heroInfo.buff_Stat.speed) * heroInfo.moveDir);
            yield return new WaitForFixedUpdate();
        }
    }

    protected IEnumerator Move(Vector3 destination)
    {
        if (heroInfo.skeletonAnimation.skeleton != null)
            heroInfo.skeletonAnimation.state.AddAnimation(0,heroInfo.castleData.code + "_Move", true, 0);//idle
        heroInfo.action = Soldier_Action.Move;
        while (transform.position != destination && heroInfo.action == Soldier_Action.Move)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * (((HeroData)heroInfo.castleData).speed + heroInfo.buff_Stat.speed));
            yield return new WaitForFixedUpdate();
        }
        heroInfo.action = Soldier_Action.Idle;
        heroInfo.skeletonAnimation.state.SetAnimation(0, heroInfo.castleData.code + "_Idle", true);
    }
}
