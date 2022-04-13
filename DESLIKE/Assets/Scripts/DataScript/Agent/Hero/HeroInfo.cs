using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class HeroInfo : CastleInfo
{
    public SkeletonAnimation skeletonAnimation;
    public SoldierBasic soldierBasic;

    public Soldier_State state;
    public Soldier_Action action;
    public Team team;

    public GameObject target;//target을 하나 더 추가해서 현재 목표중인 오브젝트 넣어주기
    public HeroInfo targetInfo;
    public GameObject skillTarget;
    public HeroInfo skillTargetInfo;

    public Vector3 moveDir;

    public float cur_Mp;
    public float barrier;
    public float healWeight = 0;

    public bool resurrection;

    public Buff_Stat buff_Stat;

    public Dictionary<string, List<Coroutine>> buffCoroutine = new Dictionary<string, List<Coroutine>>();
    public Dictionary<string, List<Coroutine>> debuffCoroutine = new Dictionary<string, List<Coroutine>>();

    void Start()
    {
        SaveManager saveManager = SaveManager.Instance;
        healWeight = 0;
        cur_Hp = saveManager.gameData.heroSaveData.cur_Hp;
        cur_Mp = saveManager.gameData.heroSaveData.cur_Mp;
        resurrection = saveManager.gameData.heroSaveData.resurrection;
        StartCoroutine(Hp_Mp_Re());
        allyPortDatas.spawnSoldierList.Add(this);
    }

    public void Stun(float stunTime)//스턴 당하기
    {
        state = Soldier_State.Stun;
        soldierBasic.StopAllCoroutines();
        StartCoroutine(soldierBasic.Stun_Behaviour(stunTime));
    }

    public void Taunt(HeroInfo _targetInfo, float tauntTime)//도발 당하기
    {
        state = Soldier_State.Taunt;
        soldierBasic.StopAllCoroutines();
        StartCoroutine(soldierBasic.Taunt_Behaviour(_targetInfo, tauntTime));
    }

    protected IEnumerator Hp_Mp_Re()
    {
        while (true)
        {
            if (cur_Hp + (((HeroData)castleData).hp_Re + buff_Stat.hp_Re) >= castleData.hp)
            {
                cur_Hp = castleData.hp;
            }
            else
            {
                cur_Hp += (((HeroData)castleData).hp_Re + buff_Stat.hp_Re);
            }
            if (cur_Mp + (((HeroData)castleData).mp_Re + buff_Stat.mp_Re) >= ((HeroData)castleData).mp)
            {
                cur_Mp = ((HeroData)castleData).mp;
            }
            else
            {
                cur_Mp += (((HeroData)castleData).mp_Re + buff_Stat.mp_Re);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    public bool TargetCheck(GameObject target, float range)
    {
        if (target != null && target.layer != 7)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject FindNearestSoldier(Collider2D[] targets)
    {
        float minDistance = 10e9F;
        GameObject target = null;
        foreach (Collider2D collider in targets)
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

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        healWeight += 1;//계산식 만들어서 변경
    }

    public override void OnHealed(float heal)
    {
        base.OnHealed(heal);
        healWeight -= 1;//계산식 만들어서 변경
    }
}