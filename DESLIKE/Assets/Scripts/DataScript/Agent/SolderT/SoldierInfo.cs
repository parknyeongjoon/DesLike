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
        BattleUIManager.Instance.UpdateSoldierRatioBar();
        afterDeadHandler += Dead;
        StartCoroutine(Hp_Mp_Re());
    }

    void Dead()
    {
        gameObject.layer = 7;
        animator.SetTrigger("isDead");
        AkSoundEngine.PostEvent("skeleton_Dead", gameObject);
        portDatas.spawnSoldierList.Remove(this);
        BattleUIManager.Instance.UpdateSoldierRatioBar();
        if (portDatas.spawnSoldierList.Count == 0)
        {
            BattleUIManager.Instance.EndStage();
        }
        Destroy(gameObject, 10.0f);
    }

    void OnMouseDown()
    {
        if(MouseManager.Instance.mouseState == Mouse_State.Idle && gameObject.layer != 7)
        {
            if (MouseManager.Instance.mouseFocus != null)
            {
                MouseManager.Instance.mouseFocus.SetActive(false);
            }
            MouseManager.Instance.mouseFocus = transform.Find("mouseFocus").gameObject;
            MouseManager.Instance.mouseFocus.SetActive(true);
            BattleUIManager.Instance.cur_Soldier = GetComponent<SoldierInfo>();
            BattleUIManager.Instance.SetMidPanel(0);
        }
    }
}