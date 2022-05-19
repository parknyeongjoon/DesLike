using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierInfo : HeroInfo
{
    public string soldierCode;

    protected override IEnumerator Start()
    {
        if (team == Team.Ally) { castleData = allyPortDatas.activeSoldierList[soldierCode]; }
        cur_Hp = castleData.hp;
        cur_Mp = 0;
        allyPortDatas.spawnSoldierList.Add(this);
        AddInfoList();
        castleData.extraSkills?.Invoke(this);
        BattleUIManager.Instance.UpdateSoldierRatioBar();
        afterDeadEvent.AddListener(Dead);
        yield return new WaitUntil(() => BattleUIManager.Instance.battleStart);//배틀 스타트 될 때까지 기다리기
        StartCoroutine(Hp_Mp_Re());
    }

    void AddInfoList()
    {
        switch (((SoldierData)castleData).soldier_Type)
        {
            case Soldier_Type.Tanker:
            case Soldier_Type.Soldier:
            case Soldier_Type.Monster:
                allyPortDatas.meleeSoldierList.Add(this);
                break;
            case Soldier_Type.Ranger:
            case Soldier_Type.Magician:
            case Soldier_Type.Healer:
            case Soldier_Type.Buffer:
            case Soldier_Type.Debuffer:
                allyPortDatas.rangerSoldierList.Add(this);
                break;
            case Soldier_Type.Catapult:
                allyPortDatas.catapultSoldierList.Add(this);
                break;
        }
    }

    void Dead(HeroInfo heroInfo)
    {
        gameObject.layer = 7;
        transform.position += new Vector3(0, 0, 1);
        if (skeletonAnimation.skeleton != null)
            skeletonAnimation.state.SetAnimation(0, soldierCode + "_Die", false);
        //AkSoundEngine.PostEvent(soldierCode + "_Die", gameObject);활성화
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
            BattleUIManager.Instance.cur_Soldier = this;
            BattleUIManager.Instance.SetMidPanel(0);
        }
    }
}