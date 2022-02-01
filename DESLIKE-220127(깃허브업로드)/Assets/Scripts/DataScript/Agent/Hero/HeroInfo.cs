using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroInfo : CastleInfo
{
    public HeroData heroData;
    public CastleInfo targetInfo;
    public GameObject target;
    public float cur_Mp;
    public Vector3 moveDir;
    public Soldier_State state;
    public Soldier_Action action;
    public Team team;
    public float healWeight;
    public bool resurrection;
    [SerializeField]
    PortDatas allyPortDatas;

    void Start()
    {
        SaveManager saveManager = SaveManager.Instance;
        healWeight = 0;
        heroData = (HeroData)castleData;
        cur_Hp = saveManager.gameData.heroSaveData.cur_Hp;
        cur_Mp = saveManager.gameData.heroSaveData.cur_Mp;
        resurrection = saveManager.gameData.heroSaveData.resurrection;
        //allyPortDatas.spawnSoldierList.Add(this);
    }

    public bool TargetCheck(float range)
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range + targetInfo.castleData.size)//몸의 중심 말고 테투리부터 거리
            {
                return true;
            }
        }
        return false;
    }
}
