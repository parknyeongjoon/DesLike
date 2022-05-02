using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyEvent : EventBasic
{
    int temp_Max_HP = 500;
    RelicManager relicManager;
    int ranPenalty;  // 0 : 체력, 1 : 포트, 2 : 특화 손실, 3, 골드 손실

    void OnEnable()
    {
        SetOption();
        relicManager = RelicManager.Instance;
        isEventSet = true;
    }

    void SetOption()
    {
        if (isAlreadySelect == false)   // 이미 선택되지 않았을 때만 세팅
        {
            if (isEventSet == false)
            {

                ranPenalty = Random.Range(0, 4);
            }
            else
                ranPenalty = saveManager.gameData.eventData.ranPenalty;

            switch (ranPenalty)
            {
                case 0: // 체력
                    OptionText[0].text = "1일 소모, 체력 30% 손실";
                    OptionText[1].text = "2일 소모, 체력 20% 손실";
                    OptionText[2].text = "3일 소모, 체력 10% 손실";
                    break;

                case 1: // 포트
                    OptionText[0].text = "1일 소모, 포트 1칸 손실";
                    OptionText[1].text = "2일 소모, 50% 확률로 포트 손실";
                    OptionText[2].text = "3일 소모, 포트 손실X";
                    break;

                case 2: // 특화 손실
                    OptionText[0].text = "1일 소모, 특화 1마리 확정 손실";
                    OptionText[1].text = "2일 소모, 50%확률로 특화 1마리 손실";
                    OptionText[2].text = "3일 소모, 특화 손실 X";
                    break;

                case 3: // 골드 손실
                    OptionText[0].text = "1일 소모, 70골드 손실";
                    OptionText[1].text = "2일 소모, 50골드 손실";
                    OptionText[2].text = "3일 소모, 30골드 손실";
                    break;

                default:
                    OptionText[0].text = "오류 떴다";
                    OptionText[1].text = "일해라";
                    OptionText[2].text = "김시후";
                    break;
            }
        }
    }
    
    void HPPenalty(int button)
    {
        switch (button)
        {
            case 0:
                cur_HP = cur_HP / 10 * 7;
                break;

            case 1:
                cur_HP = cur_HP / 10 * 8;
                break;

            case 2:
                cur_HP = cur_HP / 10 * 9;
                break;
            default:
                Debug.Log("HP패널티 오류. 일해라 김시후");
                break;
        }
    }

    void PortPenalty(int button)
    {
        switch (button)
        {
            case 0:
                // 포트 패널티
                break;

            case 1:
                int portRand = Random.Range(0, 2);
                // portRand 이용해서 포트 패널티
                break;

            case 2:
                break;

            default:
                Debug.Log("포트패널티 오류. 일해라 김시후");
                break;
        }
    }

    void MutantPenalty(int button)
    {
        switch (button)
        {
            case 0:
                // 특화 패널티
                break;

            case 1:
                int mutRand = Random.Range(0, 2);
                // 50% 확률로 특화패널티
                break;

            case 2:
                break;

            default:
                Debug.Log("특화패널티 오류. 일해라 김시후");
                break;
        }
    }

    void GoldPenalty(int button)
    {
        switch (button)
        {
            case 0:
                curGold -= 70;
                break;

            case 1:
                curGold -= 50;
                break;

            case 2:
                curGold -= 30;
                break;

            default:
                Debug.Log("골드패널티 오류. 일해라 김시후");
                break;
        }
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    
    // 버튼용 함수
    public void Button1()
    {
        curDay += 1;
        switch(ranPenalty)
        {
            case 0:
                HPPenalty(0);
                break;
            case 1:
                PortPenalty(0);
                break;
            case 2:
                MutantPenalty(0);
                break;
            case 3:
                GoldPenalty(0);
                break;
            default:
                Debug.Log("버튼1 오류. 일해라 김시후");
                break;
        }
        ButtonsOff();
    }

    public void Button2()
    {
        switch (ranPenalty)
        {
            case 0:
                HPPenalty(1);
                break;
            case 1:
                PortPenalty(1);
                break;
            case 2:
                MutantPenalty(1);
                break;
            case 3:
                GoldPenalty(1);
                break;
            default:
                Debug.Log("버튼2 오류. 일해라 김시후");
                break;
        }
        ButtonsOff();
    }

    public void Button3()
    {
        switch (ranPenalty)
        {
            case 0:
                HPPenalty(2);
                break;
            case 1:
                PortPenalty(2);
                break;
            case 2:
                MutantPenalty(2);
                break;
            case 3:
                GoldPenalty(2);
                break;
            default:
                Debug.Log("버튼3 오류. 일해라 김시후");
                break;
        }
        ButtonsOff();
    }
}
