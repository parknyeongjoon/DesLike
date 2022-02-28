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
        StartCoroutine(Hp_Mp_Re());
    }

    void Dead()
    {
        gameObject.GetComponent<SoldierBehaviour>().StopAllCoroutines();//unityEvent로 넣기
        gameObject.layer = 7;
        animator.SetTrigger("isDead");
        portDatas.spawnSoldierList.Remove(this);
        Destroy(gameObject, 10.0f);
    }
}