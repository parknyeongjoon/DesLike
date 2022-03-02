using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    static BattleUIManager instance;

    [SerializeField]
    GameObject PortPanel, HeroPanel, SoldierPanel, ProducePanel, SetPortPanel, EmptyPanel, ChallengePanel, InfoPanel, RewardPanel;
    [SerializeField]
    Button start_Btn;
    public PortData cur_Port;
    public SoldierInfo cur_Soldier;

    public SoldierPanel soldierPanel;
    public SetPortPanel setPortPanel;
    public RewardPanel rewardPanel;
    public HeroPanel heroPanel;
    public InfoPanel infoPanel;
    public ChallengePanel challengePanel;

    void Awake()
    {
        instance = this;
        MouseManager.Instance.battleUIManager = this;

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

    //0 병사 패널 1생성 패널 2포트 정보 패널 3끄기 4챌린지 패널
    public void SetMidPanel(int index)//string 매개변수 가능? 가독성 너무 떨어짐
    {
        SoldierPanel.SetActive(false);
        ProducePanel.SetActive(false);
        SetPortPanel.SetActive(false);
        ChallengePanel.SetActive(false);
        EmptyPanel.SetActive(false);
        switch (index)
        {
            case 0:
                SoldierPanel.gameObject.SetActive(true);
                break;
            case 1:
                ProducePanel.gameObject.SetActive(true);
                break;
            case 2:
                SetPortPanel.gameObject.SetActive(true);
                break;
            case 3:
                EmptyPanel.gameObject.SetActive(true);
                break;
            case 4:
                ChallengePanel.gameObject.SetActive(true);
                break;
        }
    }

    public void ClickPort()
    {
        if (cur_Port.soldierCode == null)
        {
            SetMidPanel(1);
        }
        else
        {
            SetMidPanel(2);
        }
    }

    public void SetRewardPanel()
    {
        RewardPanel.gameObject.SetActive(true);
        rewardPanel.SetRewardPanel();
    }

    public void RemoveStartBtn()
    {
        start_Btn.gameObject.SetActive(false);
    }
}
