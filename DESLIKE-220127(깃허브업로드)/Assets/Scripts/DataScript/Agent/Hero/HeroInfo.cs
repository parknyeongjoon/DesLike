using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroInfo : CastleInfo
{
    public CastleInfo targetInfo;
    public GameObject target;
    public float cur_Mp;
    public Vector3 moveDir;
    public Soldier_State state;
    public Soldier_Action action;
    public Team team;
    public float healWeight;
    public bool resurrection;
    public Buff_Stat buff_Stat;

    public Animator animator;

    public Dictionary<string, Coroutine> buffCoroutine;
    public Dictionary<string, Coroutine> debuffCoroutine;

    void Start()
    {
        SaveManager saveManager = SaveManager.Instance;
        healWeight = 0;
        cur_Hp = saveManager.gameData.heroSaveData.cur_Hp;
        cur_Mp = saveManager.gameData.heroSaveData.cur_Mp;
        resurrection = saveManager.gameData.heroSaveData.resurrection;
        //allyPortDatas.spawnSoldierList.Add(this);
    }

    public void Stun(float stunTime)
    {
        state = Soldier_State.Stun;
        //StartCoroutine(soldierBehaviour.Stun_Behaviour());
    }

    public bool TargetCheck(float range)
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)//몸의 중심 말고 테투리부터 거리 targetInfo.castleData.size
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
}
