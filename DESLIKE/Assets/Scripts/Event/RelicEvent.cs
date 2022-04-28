using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RelicEvent : MonoBehaviour
{
    bool isEventSet;
    SaveManager saveManager;
    int[] optionNum = new int[3];
    EventData eventData = new EventData();
    public TMP_Text[] OptionText = new TMP_Text[3];
    public Button[] Buttons = new Button[3];
    public Button EndButton;
    int curDay;
    bool isAlreadySelect; // 이미 이벤트 선택했는지 함수

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        DataLoad();
        SetOption();
    }

    void DataLoad()
    {
        isEventSet = eventData.isEventSet;
        eventData = saveManager.gameData.eventData;
        curDay = saveManager.gameData.mapData.curDay;
        isAlreadySelect = eventData.isAlreadySelect;
    }

    void ButtonOnOff()
    {
        if (isAlreadySelect == false)
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(true);
            EndButton.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(false);
            EndButton.gameObject.SetActive(true);
        }
    }

    void SetOption()
    {
        if (isAlreadySelect == false)   // 이미 선택되지 않았을 때만 세팅
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
                    saveManager.gameData.eventData.optionNum[i] = optionNum[i];
                }
            }
            else  // 데이터 가져오기
            {
                for (int i = 0; i < 3; i++)
                {
                    optionNum[i] = eventData.optionNum[i];
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
    }

    void EventOption4(int optionNum)    // 4번 보기
    {
        OptionText[optionNum].text = "2일 소모, 최대 체력의 n% 잃고 희귀 유물 획득";
    }

    void EventOption5(int optionNum)    // 5번 보기
    {
        OptionText[optionNum].text = "3일 소모, 최대 체력의 n% + n골드 잃고 전설 유물 획득";
        // 가진 골드가 n골드보다 작다면 버튼 비활성화
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
        saveManager.gameData.mapData.curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"2일 소모, 일반 유물 획득"
    {
        saveManager.gameData.mapData.curDay += 2;
        // 일반 유물 획득 함수
        ButtonsOff();
    }

    void ActiveEvent3() //"2일 소모, n골드 잃고 희귀 유물 획득"
    {
        saveManager.gameData.mapData.curDay += 2;
        // 골드 손실 함수
        // 희귀 유물 획득 함수
        ButtonsOff();
    }

    void ActiveEvent4() //"2일 소모, 최대 체력의 n% 잃고 희귀 유물 획득"
    {
        saveManager.gameData.mapData.curDay += 2;
        // 최대체력의 n% 잃는 함수, 만약 현재 체력이 n%보다 작다면 현재 체력을 1로 만듦
        // 희귀 유물 획득 함수
        ButtonsOff();
    }

    void ActiveEvent5() //"3일 소모, 최대 체력의 n% + n골드 잃고 전설 유물 획득"
    {
        saveManager.gameData.mapData.curDay += 3;
        // 최대체력의 n% 잃는 함수, 만약 현재 체력이 n%보다 작다면 현재 체력을 1로 만듦
        // 골드 손실 함수
        // 전설 유물 획득 함수
        ButtonsOff();
    }

    void ActiveEvent6() //"1일 소모, 골드를 모두 잃고 희귀 유물 획득"
    {
        saveManager.gameData.mapData.curDay += 1;
        // 골드 손실 함수
        // 희귀 유물 획득 함수
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
    }

    public void EndEvent()
    {
        
        SceneManager.LoadScene("Map");
    }
}
