using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierInfo : HeroInfo
{
    public string soldierCode;

    void Start()
    {
        castleData = allyPortDatas.activeSoldierList[soldierCode];
        cur_Hp = castleData.hp;
        cur_Mp = ((SoldierData)castleData).mp;
        allyPortDatas.spawnSoldierList.Add(this);
        BattleUIManager.Instance.UpdateSoldierRatioBar();
        afterDeadEvent.AddListener(Dead);
        StartCoroutine(Hp_Mp_Re());
    }

    void Dead()
    {
        gameObject.layer = 7;
        if (skeletonAnimation.skeleton != null)
            skeletonAnimation.state.SetAnimation(0, "H_23101_Die", false);
        AkSoundEngine.PostEvent("T_23101_Die", gameObject);
        Debug.Log("Dead");
        allyPortDatas.spawnSoldierList.Remove(this);
        BattleUIManager.Instance.UpdateSoldierRatioBar();
        if (allyPortDatas.spawnSoldierList.Count == 0)
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