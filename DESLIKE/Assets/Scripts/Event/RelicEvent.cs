using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RelicEvent : EventBasic
{
    int curGold;
    int[] optionNum = new int[3];
    float cur_HP, max_HP;
    // string[] relicsCode = new string[3];
    bool isEventSet, isAlreadySelect;

    Kingdom kingdom;
    RelicManager relicManager;
 
    [SerializeField] Button[] Buttons = new Button[3];
    [SerializeField] TMP_Text[] OptionText = new TMP_Text[3];
    
    void OnEnable()
    {
        relicManager = RelicManager.Instance;

        LoadData();
        SetOption();
        // 세팅 다 했다고 표시
        isEventSet = true;
        eventEnd = false;

        SaveData();
    }

    void SaveData()
    {
        SaveRelEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SaveRelEData()
    {
        saveManager.gameData.heroSaveData.cur_Hp = cur_HP;
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.isEventSet = isEventSet;
        saveManager.gameData.eventData.isAlreadySelect = isAlreadySelect;

        for (int i = 0; i < 3; i++)
            saveManager.gameData.eventData.optionNum[i] = optionNum[i];
    }

    void LoadData()
    {
        LoadRelEData();
        LoadComData();
    }

    void LoadRelEData()
    {
        kingdom = saveManager.gameData.mapData.kingdom;
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        max_HP = saveManager.dataSheet.heroDataSheet[saveManager.gameData.heroSaveData.heroCode].hp;
        curGold = saveManager.gameData.goodsSaveData.gold;
        isEventSet = saveManager.gameData.eventData.isEventSet;
        isAlreadySelect = saveManager.gameData.eventData.isAlreadySelect;

        for (int i = 0; i < 3; i++)
            optionNum[i] = saveManager.gameData.eventData.optionNum[i];
    }

    void SetOption()
    {
        if (isAlreadySelect == false)   // 이미 선택되지 않았을 때(이벤트 종료맵이 아닐 때)만 세팅
        {
            if (isEventSet == false)    // 처음이라면 새로 설정
            {
                for (int i = 0; i < 3; i++)
                {
                Reroll:
                    optionNum[i] = Random.Range(0, 6);  // 선택지 갯수만큼 랜덤값 설정(임시값 6)
                    if ((i == 1) && (optionNum[0] == optionNum[1]))
                        goto Reroll;  // 같은 값인지 체크
                    if ((i == 2) && ((optionNum[0] == optionNum[2]) || (optionNum[1] == optionNum[2]))) // 1,2 선택지와 3 선택지가 같은지 체크
                        goto Reroll;  // 같은 값인지 체크
                }
            }
          
            for (int i = 0; i < 3; i++)    // 텍스트 변경용
            {
                switch (optionNum[i] + 1)
                {
                    case 1:
                        EventOption1(i);
                        break;
                    case 2:
                        EventOption2(i);
                        break;
                    case 3:
                        EventOption3(i);
                        break;
                    case 4:
                        EventOption4(i);
                        break;
                    case 5:
                        EventOption5(i);
                        break;
                    case 6:
                        EventOption6(i);
                        break;
                }
            }
        }
        else
        {
            ButtonsOff();
        }
    }

    void EventOption1(int optionNum)    // 1번 보기
    {
        OptionText[optionNum].text = "1일 소모, 유물 획득X";
    }

    void EventOption2(int optionNum)    // 2번 보기
    {
        OptionText[optionNum].text = "2일 소모, 일반 유물 획득";
    }

    void EventOption3(int optionNum)    // 3번 보기
    {
        OptionText[optionNum].text = "2일 소모, n골드 잃고 희귀 유물 획득";
        // 가진 골드가 n골드보다 작다면 버튼 비활성화
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void EventOption4(int optionNum)    // 4번 보기
    {
        OptionText[optionNum].text = "2일 소모, 최대 체력의 n% 잃고 희귀 유물 획득";
    }

    void EventOption5(int optionNum)    // 5번 보기
    {
        OptionText[optionNum].text = "3일 소모, 최대 체력의 n% + n골드 잃고 전설 유물 획득";
        // 가진 골드가 n골드보다 작다면 버튼 비활성화
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void EventOption6(int optionNum)    // 6번 보기
    {
        OptionText[optionNum].text = "1일 소모, 골드를 모두 잃고 희귀 유물 획득";
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    void ActiveEvent1() //"1일 소모, 유물 획득X"
    {
        curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"2일 소모, 일반 유물 획득"
    {
        curDay += 2;
        // relicManager.GetRelic(eventNode.SetNorRel(kingdom));
        // RelicPanel에다가 instantiate

       
        // 일반 유물 획득 함수 (변경 필요)
        ButtonsOff();
    }

    void ActiveEvent3() //"2일 소모, n골드 잃고 희귀 유물 획득"
    {
        curDay += 2;
        curGold -= 50; // 골드 손실 함수
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent4() //"2일 소모, 최대 체력의 n% 잃고 희귀 유물 획득"
    {
        curDay += 2;

        cur_HP -= max_HP / 10; // 10% 임의 설정
        if (cur_HP < 1) cur_HP = 1;
        // 최대체력의 n% 잃는 함수, 만약 현재 체력이 n%보다 작다면 현재 체력을 1로 만듦
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent5() //"3일 소모, 최대 체력의 n% + n골드 잃고 전설 유물 획득"
    {
        curDay += 3;
        cur_HP -= max_HP / 10;
        if (cur_HP < 1) cur_HP = 1;
        // 최대체력의 n% 잃는 함수, 만약 현재 체력이 n%보다 작다면 현재 체력을 1로 만듦
        curGold -= 50; // 골드 손실 함수
        // relicManager.GetRelic(eventNode.SetLegRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent6() //"1일 소모, 골드를 모두 잃고 희귀 유물 획득"
    {
        curDay += 1;
        curGold = 0; // 골드 손실 함수
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));


        ButtonsOff();
    }


    // 버튼용 함수
    public void Button1()
    {
        switch (optionNum[0] + 1)
        {
            case 1:
                ActiveEvent1();
                break;
            case 2:
                ActiveEvent2();
                break;
            case 3:
                ActiveEvent3();
                break;
            case 4:
                ActiveEvent4();
                break;
            case 5:
                ActiveEvent5();
                break;
            case 6:
                ActiveEvent6();
                break;
        }
        isAlreadySelect = true;
        isEventSet = false;
        SaveComData();
    }

    public void Button2()
    {
        switch (optionNum[1] + 1)
        {
            case 1:
                ActiveEvent1();
                break;
            case 2:
                ActiveEvent2();
                break;
            case 3:
                ActiveEvent3();
                break;
            case 4:
                ActiveEvent4();
                break;
            case 5:
                ActiveEvent5();
                break;
            case 6:
                ActiveEvent6();
                break;
        }
        isAlreadySelect = true;
        isEventSet = false;
        SaveData();
    }

    public void Button3()
    {
        switch (optionNum[2] + 1)
        {
            case 1:
                ActiveEvent1();
                break;
            case 2:
                ActiveEvent2();
                break;
            case 3:
                ActiveEvent3();
                break;
            case 4:
                ActiveEvent4();
                break;
            case 5:
                ActiveEvent5();
                break;
            case 6:
                ActiveEvent6();
                break;
        }
        isAlreadySelect = true;
        isEventSet = false;
        SaveData();
    }
}
