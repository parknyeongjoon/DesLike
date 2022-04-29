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
        if (isAlreadySelect == false)   // �̹� ���õ��� �ʾ��� ���� ����
        {
            if (isEventSet == false)    // ó���̶�� ���� ����
            {
                for (int i = 0; i < 3; i++)
                {
                Reroll:
                    optionNum[i] = Random.Range(0, 6);  // ������ ������ŭ ������ ����(�ӽð� 6)
                    if ((i == 1) && (optionNum[0] == optionNum[1]))
                        goto Reroll;  // ���� ������ üũ
                    if ((i == 2) && ((optionNum[0] == optionNum[2]) || (optionNum[1] == optionNum[2]))) // 1,2 �������� 3 �������� ������ üũ
                        goto Reroll;  // ���� ������ üũ
                                      // ���� ����

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
            else  // ������ ��������
            {
                for (int i = 0; i < 3; i++)
                {
                    optionNum[i] = eventData.optionNum[i];
                }
            }

            for (int i = 0; i < 3; i++)    // �ؽ�Ʈ �����
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

    void EventOption1(int optionNum)    // 1�� ����
    {
        OptionText[optionNum].text = "1�� �Ҹ�, ȸ��X";
    }

    void EventOption2(int optionNum)    // 2�� ����
    {
        OptionText[optionNum].text = "1�� �Ҹ�, n��� �Һ�, ü�� 10% ȸ��";
    }

    void EventOption3(int optionNum)    // 3�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, ü�� 10% ȸ��";
    }

    void EventOption4(int optionNum)    // 4�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, n��� �Һ�, ü�� 20% ȸ��";
        // ���� ��尡 n��庸�� �۴ٸ� ��ư ��Ȱ��ȭ
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void EventOption5(int optionNum)    // 5�� ����
    {
        OptionText[optionNum].text = "3�� �Ҹ�, ü�� 20% ȸ��";
    }

    void EventOption6(int optionNum)    // 6�� ����
    {
        OptionText[optionNum].text = "3�� �Ҹ�, n��� �Һ�, ü�� 30% ȸ��";
        // ���� ��尡 n��庸�� �۴ٸ� ��ư ��Ȱ��ȭ
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    void ActiveEvent1() //"1�� �Ҹ�, ȸ��X"
    {
        saveManager.gameData.mapData.curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"1�� �Ҹ�, n��� �Һ�, ü�� 10% ȸ��"
    {
        saveManager.gameData.mapData.curDay += 1;
        curGold -= 50;        // n��� �ս� �Լ�
        cur_HP = cur_HP / 9 * 10;   // ȸ�� �Լ�
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // �ִ�ü�� �ʰ� �� �ִ�ü�� ǥ�� 
        // heroData �ذ�Ǹ� temp_Max_HP�� heroData.hp�� ����
        ButtonsOff();
    }

    void ActiveEvent3() //"2�� �Ҹ�, ü�� 10% ȸ��"
    {
        saveManager.gameData.mapData.curDay += 2;
        cur_HP = cur_HP / 9 * 10;   // ȸ�� �Լ�
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // �ִ�ü�� �ʰ� �� �ִ�ü�� ǥ��

        ButtonsOff();
    }

    void ActiveEvent4() //"2�� �Ҹ�, n��� �Һ�, ü�� 20% ȸ��"
    {
        saveManager.gameData.mapData.curDay += 2;
        curGold -= 50;        // n��� �ս� �Լ�
        cur_HP = cur_HP / 4 * 5;    // ȸ�� �Լ�
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // �ִ�ü�� �ʰ� �� �ִ�ü�� ǥ��
        ButtonsOff();
    }

    void ActiveEvent5() //"3�� �Ҹ�, ü�� 20% ȸ��"
    {
        saveManager.gameData.mapData.curDay += 3;
        cur_HP = cur_HP / 4 * 5;    // ȸ�� �Լ�
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // �ִ�ü�� �ʰ� �� �ִ�ü�� ǥ��
                                                        
        ButtonsOff();
    }

    void ActiveEvent6() //"3�� �Ҹ�, n��� �Һ�, ü�� 30% ȸ��"
    {
        saveManager.gameData.mapData.curDay += 3;
        curGold -= 50;        // n��� �ս� �Լ�
        cur_HP = cur_HP / 7 * 10;
        if (cur_HP > temp_Max_HP) cur_HP = temp_Max_HP; // �ִ�ü�� �ʰ� �� �ִ�ü�� ǥ��

        ButtonsOff();
    }


    // ��ư�� �Լ�
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
