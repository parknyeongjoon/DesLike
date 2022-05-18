using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GambleEvent : EventBasic
{
    int curGold, rewardNum, comBox;  // 보상으로 줘야하는 골드/체력의 액수
    int[] choiNum = new int[3];  // 0 : 1번째 선택지(골드, 체력, X), 1 : 2번째 선택지(액수), 2 : 도박 선택
    float cur_HP, max_HP;
    bool[] stepCheck = new bool[5];

    [SerializeField] TMP_Text InformText;
    [SerializeField] Button[] Buttons = new Button[3];
    [SerializeField] TMP_Text[] OptionText = new TMP_Text[3];

    void OnEnable()
    {
        LoadData();
        SetOption();
        eventEnd = false;
        SaveData();
    }

    void SaveData()
    {
        SaveGambEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SaveGambEData()
    {
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.rewardNum = rewardNum;
        saveManager.gameData.eventData.comBox = comBox;
        saveManager.gameData.heroSaveData.cur_Hp = cur_HP;
        saveManager.dataSheet.heroDataSheet[saveManager.gameData.heroSaveData.heroCode].hp = max_HP;

        for (int i = 0; i < 5; i++)
        {
            if (i < 3) saveManager.gameData.eventData.choiNum[i] = choiNum[i];
            saveManager.gameData.eventData.stepCheck[i] = stepCheck[i];
        }
    }

    void LoadData()
    {
        LoadGambEData();
        LoadComData();
    }

    void LoadGambEData()
    {
        curGold = saveManager.gameData.goodsSaveData.gold;
        rewardNum = saveManager.gameData.eventData.rewardNum;
        comBox = saveManager.gameData.eventData.comBox;
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        max_HP = saveManager.dataSheet.heroDataSheet[saveManager.gameData.heroSaveData.heroCode].hp;
        
        for (int i = 0; i < 5; i++)
        {
            if (i < 3) choiNum[i] = saveManager.gameData.eventData.choiNum[i];
            stepCheck[i] = saveManager.gameData.eventData.stepCheck[i];
        }
    }

    void SetOption()
    {
        if (stepCheck[0] == false)
            InitSet();
        else if (stepCheck[1] == false)
            FirstSelect(choiNum[0]);
        else if (stepCheck[2] == false)
            SecondSelect(choiNum[1]);
        else if (stepCheck[3] == false)
            ThirdSelect(choiNum[2]);
        else if (stepCheck[4] == false)
            Continue();
    }

    void InitSet()
    {
        for (int i = 0; i < 3; i++)
        {
            Buttons[i].gameObject.SetActive(true);
        }
        InformText.text = "확률은 3분의 1, 보상은 3배! 게임 한 번 해보시지 않겠습니까?";
        OptionText[0].text = "골드를 건다.(보상 : 건 골드 3배, 3일 소모)";
        OptionText[1].text = "체력을 건다.(보상 : 건 체력의 3배만큼 회복, 3일 소모)";
        OptionText[2].text = "도박을 하지 않는다.(이벤트 종료, 1일 소모)";
    }

    void FirstSelect(int button)  // 골드를 건다, 체력을 건다, 도박 안한다
    {
        choiNum[0] = button;
        if (stepCheck[0] == false)
        {
            switch (button)
            {
                case 0:
                    curDay += 3;
                    InformText.text = "오호, 골드를 고르셨군요. 얼마를 거시겠습니까?";
                    OptionText[0].text = "30골드 (보상 : 90골드)";
                    OptionText[1].text = "50골드 (보상 : 150골드)";
                    OptionText[2].text = "100골드 (보상 : 100골드)";
                    if (curGold < 30)   // 30원보다 적으면 가진 돈 전부
                        OptionText[0].text = curGold + "골드 (보상 : " + curGold * 3 + "골드)";
                    if (curGold < 100)
                    {
                        Buttons[2].interactable = false;    // 100골드 미만 선택지 3 삭제
                        if (curGold < 50) Buttons[1].interactable = false;    // 50골드 미만 선택지 2 삭제
                    }
                    break;
                case 1:
                    curDay += 3;
                    InformText.text = "오호, 체력을 고르셨군요. 얼마나 거시겠습니까?";
                    OptionText[0].text = "30HP (보상 : HP 90 회복)";
                    OptionText[1].text = "50HP (보상 : HP 150 회복)";
                    OptionText[2].text = "100HP (보상 : HP 100 회복)";
                    if (cur_HP < 30)   // 30HP보다 적으면 체력 1남기고 전부
                        OptionText[0].text = (cur_HP - 1) + "HP (보상 : HP " + ((cur_HP - 1) * 3) + " 회복)";
                    if (cur_HP < 100)
                    {
                        Buttons[2].interactable = false;    // 100 미만 선택지 3 삭제
                        if (cur_HP < 50) Buttons[1].interactable = false;    // 50 미만 선택지 2 삭제
                    }
                    break;
                case 2:
                    curDay += 1;
                    InformText.text = "칫, 재미없군요.얼른 가세요.";
                    for(int i = 0; i<3; i++)
                        Buttons[i].gameObject.SetActive(false);
                    EndButton.gameObject.SetActive(true);
                    break;
                default:
                    InformText.text = "오류임. 일하셈";
                    break;
            }
        }
        stepCheck[0] = true;
    }

    void SecondSelect(int button)   // 골드 : 30 50 100 / 체력 : 30 50 100
    {
        choiNum[1] = button;
        for (int i = 0; i < 3; i++)
        {
            Buttons[i].interactable = true;
        }

        if (stepCheck[1] == false)
        {
            switch (button)
            {
                case 0:
                    if (choiNum[0] == 0)   // 골드
                    {
                        if (curGold < 30) rewardNum = curGold;
                        else rewardNum = 30;    // 30
                        InformText.text = "오호, " + rewardNum + "골드를 거셨군요. 몇 번째 상자를 고르시겠습니까?";
                    }
                    else // 체력
                    {
                        if (cur_HP <= 30) rewardNum = ((int)cur_HP - 1);
                        else rewardNum = 30;    // 30

                        InformText.text = "오호, " + rewardNum + "HP를 고르셨군요. 몇 번째 상자를 거시겠습니까?";
                    }
                    break;
                case 1:
                    if (choiNum[0] == 0)   // 골드
                    {
                        rewardNum = 50;
                        InformText.text = "오호, " + rewardNum + "골드를 거셨군요. 몇 번째 상자를 고르시겠습니까?";
                    }
                    else
                    {
                        rewardNum = 50;
                        InformText.text = "오호, " + rewardNum + "HP를 고르셨군요. 몇 번째 상자를 거시겠습니까?";
                    }
                    break;
                case 2:
                    if (choiNum[0] == 0)   // 골드
                    {
                        rewardNum = 100;
                        InformText.text = "오호, " + rewardNum + "골드를 거셨군요. 몇 번째 상자를 고르시겠습니까?";
                    }
                    else
                    {
                        rewardNum = 100;
                        InformText.text = "오호, " + rewardNum + "HP를 고르셨군요. 몇 번째 상자를 거시겠습니까?";
                    }
                    break;
                default:
                    Debug.Log("오류 떴다");
                    break;
            }
        }
        comBox = Random.Range(0, 3);
        OptionText[0].text = "첫 번째 상자";
        OptionText[1].text = "두 번째 상자";
        OptionText[2].text = "세 번째 상자";
        stepCheck[1] = true;
    }

    void ThirdSelect(int button)    // 3개 중 1개 맞추면 건 것에 따라 보상 지급, 골드 : 골드; 체력 : 유물 혹은 체력회복
    {
        choiNum[1] = button;
        // 건 만큼 손실
        if (stepCheck[2] == false)
        {
            if (choiNum[0] == 0)   // 골드라면
                curGold -= rewardNum;
            else cur_HP = rewardNum;
        }
        ToOneButton();
        InformText.text = (button + 1) + "번째 상자를 고르셨군요. 이제 결과를 공개합니다!";
        OptionText[2].text = "과연..?";
        stepCheck[2] = true;
    }

    public void Continue()
    {
        if (comBox == choiNum[1])
        {
            InformText.text = "정답은 " + (comBox+1) + "번째 상자입니다! 정답을 맞췄으니 보상을 드리겠습니다.";
            if (stepCheck[3] == true)
            {
                if (choiNum[0] == 0)
                    curGold += rewardNum * 3;
                else
                {
                    cur_HP += rewardNum * 3;
                    if (cur_HP > max_HP) cur_HP = max_HP;
                }
            }
        }
        else InformText.text = "정답은 " + (comBox+1) + "번째 상자입니다! 아쉽게도 틀리셨군요. 보상은 없습니다.";

        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
        stepCheck[3] = true;
    }

    void ToOneButton()
    {
        Buttons[0].gameObject.SetActive(false);
        Buttons[1].gameObject.SetActive(false);
    }
    // 버튼용 함수
    public void Button1()
    {
        if (stepCheck[0] == false)
            FirstSelect(0);
        else if (stepCheck[1] == false)
            SecondSelect(0);
        else ThirdSelect(0);
        SaveData();
    }

    public void Button2()
    {
        if (stepCheck[0] == false)
            FirstSelect(1);
        else if (stepCheck[1] == false)
            SecondSelect(1);
        else ThirdSelect(1);
        SaveData();
    }

    public void Button3()
    {
        if (stepCheck[0] == true && choiNum[0] == 2)  // 바로 돌아간다.
        {
            for(int i = 0; i<3; i++)
            {
                Buttons[i].gameObject.SetActive(false);
            }
            EndButton.gameObject.SetActive(true);
        }

        if (stepCheck[0] == false)
            FirstSelect(2);
        else if (stepCheck[1] == false)
            SecondSelect(2);
        else if (stepCheck[2] == false)
            ThirdSelect(2);
        else Continue();
        SaveData();
    }
}
