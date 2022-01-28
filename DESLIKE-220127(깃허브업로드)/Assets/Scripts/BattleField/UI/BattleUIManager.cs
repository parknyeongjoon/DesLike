using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    static BattleUIManager instance;

    [SerializeField]
    Canvas PortCanvas, HeroCanvas, SoldierCanvas, ProduceCanvas, SetPortCanvas, InfoCanvas, RewardCanvas, ChallengeCanvas;
    [SerializeField]
    Button start_Btn;
    public PortData cur_Port;
    public SoldierInfo cur_Soldier;

    SoldierPanel soldierPanel;
    SetPortPanel setPortPanel;
    RewardPanel rewardPanel;
    HeroPanel heroPanel;
    public InfoPanel infoPanel;
    ChallengePanel challengePanel;

    void Awake()
    {
        instance = this;
        MouseManager.Instance.battleUIManager = this;

        soldierPanel = SoldierCanvas.GetComponent<SoldierPanel>();
        setPortPanel = SetPortCanvas.GetComponent<SetPortPanel>();
        rewardPanel = RewardCanvas.GetComponent<RewardPanel>();
        heroPanel = HeroCanvas.GetComponent<HeroPanel>();
        infoPanel = InfoCanvas.GetComponent<InfoPanel>();
        challengePanel = ChallengeCanvas.GetComponent<ChallengePanel>();

        SetMidPanel(3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (PortCanvas.gameObject.activeSelf == true)
            {
                PortCanvas.gameObject.SetActive(false);
                SetMidPanel(3);
            }
            else
            {
                PortCanvas.gameObject.SetActive(true);
            }
        }
        if (SoldierCanvas.gameObject.activeSelf == true)
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
        SoldierCanvas.gameObject.SetActive(false);
        ProduceCanvas.gameObject.SetActive(false);
        SetPortCanvas.gameObject.SetActive(false);
        ChallengeCanvas.gameObject.SetActive(false);
        switch (index)
        {
            case 0:
                SoldierCanvas.gameObject.SetActive(true);
                break;
            case 1:
                ProduceCanvas.gameObject.SetActive(true);
                break;
            case 2:
                SetPortCanvas.gameObject.SetActive(true);
                break;
            case 3:
                break;
            case 4:
                ChallengeCanvas.gameObject.SetActive(true);
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
        RewardCanvas.gameObject.SetActive(true);
        rewardPanel.SetRewardPanel();
    }

    public void RemoveStartBtn()
    {
        start_Btn.gameObject.SetActive(false);
    }
}
