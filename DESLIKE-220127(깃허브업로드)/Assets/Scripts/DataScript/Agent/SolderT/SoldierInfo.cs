using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierInfo : HeroInfo
{
    public string soldierCode;

    void Start()
    {
        castleData = portDatas.activeSoldierList[soldierCode];
        cur_Hp = castleData.hp;
        cur_Mp = ((SoldierData)castleData).mp;
        portDatas.spawnSoldierList.Add(this);
        afterDeadHandler += Dead;
    }

    void Dead()
    {
        StopAllCoroutines();
        portDatas.spawnSoldierList.Remove(this);
        Destroy(gameObject);
    }
}