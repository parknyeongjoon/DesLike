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
        gameObject.GetComponent<SoldierBehaviour>().StopAllCoroutines();
        gameObject.layer = 7;
        animator.SetTrigger("isDead");
        portDatas.spawnSoldierList.Remove(this);
        Debug.Log("사망");
        Destroy(gameObject, 10.0f);
    }
}