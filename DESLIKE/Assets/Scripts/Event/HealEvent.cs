using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealEvent : EventBasic
{
    int temp_Max_HP = 500;
    void OnEnable()
    {
        SetOption();
        isEventSet = true;
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
                                      // 순서 정렬

                    int temp, j = i;
                    switch (j)
                    {
                        case 2:
                            if(optionNum[2] < optionNum[1])
                            {
                                temp = optionNum[2];
                                optionNum[2] = optionNum[1];
                                optionNum[1] = temp;
                            }
                            j--;
                            continue;
                        case 1:
                            if (optionNum[1] < optionNum[0])
                            {
                                temp = optionNum[1];
                                optionNum[1] = optionNum[0];
                                optionNum[0] = temp;
                            }
                            break;
                        case 0:
                            break;
                    }
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
        OptionText[optionNum].text = "1일 소모, 회복X";
    }

    void EventOption2(int optionNum)    // 2번 보기
    {
        OptionText[optionNum].text = "1일 소모, n골드 소비, 체력 10% 회복";
    }

    void EventOption3(int optionNum)    // 3번 보기
    {
        OptionText[optionNum].text = "2일 소모, 체력 10% 회복";
    }

    void EventOption4(int optionNum)    // 4번 보기
    {
        OptionText[optionNum].text = "2일 소모, n골드 소비, 체력 20% 회복";
        // 가진 골드가 n골드보다 작다면 버튼 비활성화
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void EventOption5(int optionNum)    // 5번 보기
    {
        OptionText[optionNum].text = "3일 소모, 체력 20% 회복";
    }

    void EventOption6(int optionNum)    // 6번 보기
    {
        OptionText[optionNum].text = "3일 소모, n골드 소비, 체력 30% 회복";
        // 가진 골드가 n골드보다 작다면 버튼 비활성화
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    void ActiveEvent1() //"1일 소모, 회복X"
    {
        saveManager.gameData.mapData.curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"1일 소모, n골드 소비, 체력 10% 회복"
    {
        saveManager.gameData.mapData.curDay += 1;
        curGold -= 50;        // n골드 손실 함수
        cur_HP = cur_HP / 9 * 10;   // 회복 함수
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // 최대체력 초과 시 최대체력 표시 
        // heroData 해결되면 temp_Max_HP를 heroData.hp로 수정
        ButtonsOff();
    }

    void ActiveEvent3() //"2일 소모, 체력 10% 회복"
    {
        saveManager.gameData.mapData.curDay += 2;
        cur_HP = cur_HP / 9 * 10;   // 회복 함수
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // 최대체력 초과 시 최대체력 표시

        ButtonsOff();
    }

    void ActiveEvent4() //"2일 소모, n골드 소비, 체력 20% 회복"
    {
        saveManager.gameData.mapData.curDay += 2;
        curGold -= 50;        // n골드 손실 함수
        cur_HP = cur_HP / 4 * 5;    // 회복 함수
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // 최대체력 초과 시 최대체력 표시
        ButtonsOff();
    }

    void ActiveEvent5() //"3일 소모, 체력 20% 회복"
    {
        saveManager.gameData.mapData.curDay += 3;
        cur_HP = cur_HP / 4 * 5;    // 회복 함수
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // 최대체력 초과 시 최대체력 표시
                                                        
        ButtonsOff();
    }

    void ActiveEvent6() //"3일 소모, n골드 소비, 체력 30% 회복"
    {
        saveManager.gameData.mapData.curDay += 3;
        curGold -= 50;        // n골드 손실 함수
        cur_HP = cur_HP / 7 * 10;
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // 최대체력 초과 시 최대체력 표시

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
}
