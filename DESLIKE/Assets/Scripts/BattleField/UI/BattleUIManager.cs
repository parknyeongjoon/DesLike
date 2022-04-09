﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    static BattleUIManager instance;

    [SerializeField] GameObject PortPanel, HeroPanel, SoldierPanel, SetPortPanel, EmptyPanel, RewardPanel;
    [SerializeField] Button start_Btn;
    [SerializeField] Image soldierRatioBarImg;
    [SerializeField] PortDatas allyPortDatas, enemyPortDatas;
    public PortData cur_Port;
    public SoldierInfo cur_Soldier;

    public SoldierPanel soldierPanel;
    public SetPortPanel setPortPanel;
    public RewardPanel rewardPanel;
    public HeroPanel heroPanel;

    void Awake()
    {
        instance = this;
        SaveManager saveManager = SaveManager.Instance;//지우기

        SetMidPanel(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PortPanel.gameObject.activeSelf == true)
            {
                PortPanel.gameObject.SetActive(false);
                SetMidPanel(3);
            }
            else
            {
                PortPanel.gameObject.SetActive(true);
            }
        }
        if (SoldierPanel.gameObject.activeSelf == true)
        {
            if(cur_Soldier == null)
            {
                SetMidPanel(3);
            }
        }
    }

    public static BattleUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    //0 병사 패널 1생성 패널 2포트 정보 패널 3끄기
    public void SetMidPanel(int index)//string 매개변수 가능? 가독성 너무 떨어짐
    {
        SoldierPanel.SetActive(false);
        SetPortPanel.SetActive(false);
        EmptyPanel.SetActive(false);
        switch (index)
        {
            case 0:
                SoldierPanel.gameObject.SetActive(true);
                break;
            case 2:
                SetPortPanel.gameObject.SetActive(true);
                break;
            case 3:
                EmptyPanel.gameObject.SetActive(true);
                break;
        }
    }

    public void ClickPort()
    {
        SetMidPanel(2);
    }

    public void BattleStart()
    {
        start_Btn.gameObject.SetActive(false);
        for(int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            AkSoundEngine.PostEvent("T_" + allyPortDatas.portDatas[i].soldierCode + "Idle", gameObject);
            AkSoundEngine.PostEvent("T_" + enemyPortDatas.portDatas[i].soldierCode + "Idle", gameObject);
        }
        AkSoundEngine.PostEvent("Battle_Start", gameObject);
        GameManager.Instance.GamePause(false);
    }

    public void EndStage()
    {
        GameManager.Instance.gamePause = true;
        AkSoundEngine.PostEvent("Battle_End", gameObject);
        GameManager.Instance.GamePause(true);
        if (enemyPortDatas.spawnSoldierList.Count == 0)//승리
        {
            SetRewardPanel();
        }
        else if(allyPortDatas.spawnSoldierList.Count == 0)//패배
        {
            Debug.Log("패배");
        }
    }

    void SetRewardPanel()
    {
        RewardPanel.gameObject.SetActive(true);
        rewardPanel.SetRewardPanel();
    }

    public void Skip()//지우기
    {
        enemyPortDatas.spawnSoldierList.Clear();
        EndStage();
    }

    public void UpdateSoldierRatioBar()
    {
        soldierRatioBarImg.fillAmount = (float)allyPortDatas.spawnSoldierList.Count / (allyPortDatas.spawnSoldierList.Count + enemyPortDatas.spawnSoldierList.Count);
    }
}
