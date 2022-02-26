using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroInfo : CastleInfo
{
    public GameObject target;
    public CastleInfo targetInfo;
    public float cur_Mp;
    public Vector3 moveDir;
    public Soldier_State state;
    public Soldier_Action action;
    public Team team;
    public float healWeight;
    public bool resurrection;
    [SerializeField]
    public Buff_Stat buff_Stat;

    public Animator animator;

    public Dictionary<string, Coroutine> buffCoroutine = new Dictionary<string, Coroutine>();
    public Dictionary<string, Coroutine> debuffCoroutine = new Dictionary<string, Coroutine>();

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
}
