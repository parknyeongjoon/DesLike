using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Spine.Unity;

public class HeroInfo : CastleInfo
{
    public SkeletonAnimation skeletonAnimation;
    public SoldierBasic soldierBasic;

    public UnityEvent<float> stunEvent;
    public UnityEvent<float> tauntEvent;

    public Soldier_State state;
    public Soldier_Action action;
    public Team team;

    public GameObject target;
    public HeroInfo targetInfo;
    public GameObject skillTarget;
    public HeroInfo skillTargetInfo;

    public Vector3 moveDir;

    public float cur_Mp;
    public float shield = 0;
    public float healWeight = 0;
    public int mucus = 0;

    public bool grit;
    public bool resurrection;

    public Buff_Stat buff_Stat;

    public Dictionary<string, List<Coroutine>> buffCoroutine = new Dictionary<string, List<Coroutine>>();
    public Dictionary<string, List<Coroutine>> debuffCoroutine = new Dictionary<string, List<Coroutine>>();

    protected virtual IEnumerator Start()
    {
        SaveManager saveManager = SaveManager.Instance;
        healWeight = 0;
        cur_Hp = saveManager.gameData.heroSaveData.cur_Hp;
        cur_Mp = saveManager.gameData.heroSaveData.cur_Mp;
        resurrection = saveManager.gameData.heroSaveData.resurrection;
        allyPortDatas.spawnSoldierList.Add(this);
        yield return new WaitUntil(() => BattleUIManager.Instance.battleStart);//배틀 스타트 될 때까지 기다리기
        StartCoroutine(Hp_Mp_Re());
    }

    public void OnDamaged(HeroInfo atkHeroInfo, float damage)//피격 이벤트 일어남
    {
        beforeHitEvent?.Invoke(this, atkHeroInfo, damage);

        if (mucus > 0 && damage > 0)//frogShield가 있을 시
        {
            mucus -= 1;
            damage = 0;
        }

        if (shield <= 0)
        {
            cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        }
        else if (shield > damage)
        {
            shield -= damage;
        }
        else if (shield > 0)
        {
            damage -= shield;
            shield = 0;
            cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        }

        if (atkHeroInfo) { afterHitEvent?.Invoke(this, atkHeroInfo, damage); }//atkHeroInfo가 null이 아니라면 피격 이벤트 발동

        healthChangeEvent?.Invoke(this);

        if (castleData.blood != null)//지우기
        {
            StartCoroutine(Bleeding());
        }

        if (cur_Hp <= 0)
        {
            Debug.Log("사망");
            Die();
        }
        healWeight += 1;//수식 만들기
    }

    public void OnDamaged(float damage)//피격 이벤트가 안 일어남
    {
        beforeHitEvent?.Invoke(this, null, damage);

        if (shield > damage)
        {
            shield -= damage;
        }
        else if (shield > 0)
        {
            damage -= shield;
            shield = 0;
            cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        }
        else if (shield <= 0)
        {
            cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        }

        healthChangeEvent?.Invoke(this);

        if (cur_Hp <= 0)
        {
            Debug.Log("사망");
            Die();
        }
        healWeight += 1;//수식 만들기
    }

    public virtual void OnHealed(float heal)
    {
        if (cur_Hp + heal >= castleData.hp)
        {
            cur_Hp = castleData.hp;
        }
        else
        {
            cur_Hp += heal;
        }
        healWeight -= 1;//수식 만들기
        healthChangeEvent?.Invoke(this);
    }

    public void Stun(float stunTime)
    {
        skeletonAnimation.state.SetAnimation(0, castleData.code + "_Stun", false);
        state = Soldier_State.Stun;
        stunEvent?.Invoke(stunTime);
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
            healthChangeEvent?.Invoke(this);
            yield return new WaitForSeconds(1.0f);
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
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = collider.gameObject;
            }
        }
        return target;
    }

    //피격 유혈 효과 지우기
    IEnumerator Bleeding()
    {
        GameObject createBlood;
        createBlood = Instantiate(castleData.blood, transform);
        yield return new WaitForSeconds(0.8f);
        Destroy(createBlood);
    }
}