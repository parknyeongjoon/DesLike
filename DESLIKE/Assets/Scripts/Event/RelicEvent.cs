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
    bool isAlreadySelect; // �̹� �̺�Ʈ �����ߴ��� �Լ�

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
                    saveManager.gameData.eventData.optionNum[i] = optionNum[i];
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
        OptionText[optionNum].text = "1�� �Ҹ�, ���� ȹ��X";
    }

    void EventOption2(int optionNum)    // 2�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, �Ϲ� ���� ȹ��";
    }

    void EventOption3(int optionNum)    // 3�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, n��� �Ұ� ��� ���� ȹ��";
        // ���� ��尡 n��庸�� �۴ٸ� ��ư ��Ȱ��ȭ
    }

    void EventOption4(int optionNum)    // 4�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, �ִ� ü���� n% �Ұ� ��� ���� ȹ��";
    }

    void EventOption5(int optionNum)    // 5�� ����
    {
        OptionText[optionNum].text = "3�� �Ҹ�, �ִ� ü���� n% + n��� �Ұ� ���� ���� ȹ��";
        // ���� ��尡 n��庸�� �۴ٸ� ��ư ��Ȱ��ȭ
    }

    void EventOption6(int optionNum)    // 6�� ����
    {
        OptionText[optionNum].text = "1�� �Ҹ�, ��带 ��� �Ұ� ��� ���� ȹ��";
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    void ActiveEvent1() //"1�� �Ҹ�, ���� ȹ��X"
    {
        saveManager.gameData.mapData.curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"2�� �Ҹ�, �Ϲ� ���� ȹ��"
    {
        saveManager.gameData.mapData.curDay += 2;
        // �Ϲ� ���� ȹ�� �Լ�
        ButtonsOff();
    }

    void ActiveEvent3() //"2�� �Ҹ�, n��� �Ұ� ��� ���� ȹ��"
    {
        saveManager.gameData.mapData.curDay += 2;
        // ��� �ս� �Լ�
        // ��� ���� ȹ�� �Լ�
        ButtonsOff();
    }

    void ActiveEvent4() //"2�� �Ҹ�, �ִ� ü���� n% �Ұ� ��� ���� ȹ��"
    {
        saveManager.gameData.mapData.curDay += 2;
        // �ִ�ü���� n% �Ҵ� �Լ�, ���� ���� ü���� n%���� �۴ٸ� ���� ü���� 1�� ����
        // ��� ���� ȹ�� �Լ�
        ButtonsOff();
    }

    void ActiveEvent5() //"3�� �Ҹ�, �ִ� ü���� n% + n��� �Ұ� ���� ���� ȹ��"
    {
        saveManager.gameData.mapData.curDay += 3;
        // �ִ�ü���� n% �Ҵ� �Լ�, ���� ���� ü���� n%���� �۴ٸ� ���� ü���� 1�� ����
        // ��� �ս� �Լ�
        // ���� ���� ȹ�� �Լ�
        ButtonsOff();
    }

    void ActiveEvent6() //"1�� �Ҹ�, ��带 ��� �Ұ� ��� ���� ȹ��"
    {
        saveManager.gameData.mapData.curDay += 1;
        // ��� �ս� �Լ�
        // ��� ���� ȹ�� �Լ�
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

    public void EndEvent()
    {
        
        SceneManager.LoadScene("Map");
    }
}
