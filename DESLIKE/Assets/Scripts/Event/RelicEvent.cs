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
        // ���� �� �ߴٰ� ǥ��
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
        if (isAlreadySelect == false)   // �̹� ���õ��� �ʾ��� ��(�̺�Ʈ ������� �ƴ� ��)�� ����
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
        else
        {
            ButtonsOff();
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
        if (curGold < 50) Buttons[optionNum].interactable = false;
    }

    void EventOption4(int optionNum)    // 4�� ����
    {
        OptionText[optionNum].text = "2�� �Ҹ�, �ִ� ü���� n% �Ұ� ��� ���� ȹ��";
    }

    void EventOption5(int optionNum)    // 5�� ����
    {
        OptionText[optionNum].text = "3�� �Ҹ�, �ִ� ü���� n% + n��� �Ұ� ���� ���� ȹ��";
        // ���� ��尡 n��庸�� �۴ٸ� ��ư ��Ȱ��ȭ
        if (curGold < 50) Buttons[optionNum].interactable = false;
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
        curDay += 1;
        ButtonsOff();
    }

    void ActiveEvent2() //"2�� �Ҹ�, �Ϲ� ���� ȹ��"
    {
        curDay += 2;
        // relicManager.GetRelic(eventNode.SetNorRel(kingdom));
        // RelicPanel���ٰ� instantiate

       
        // �Ϲ� ���� ȹ�� �Լ� (���� �ʿ�)
        ButtonsOff();
    }

    void ActiveEvent3() //"2�� �Ҹ�, n��� �Ұ� ��� ���� ȹ��"
    {
        curDay += 2;
        curGold -= 50; // ��� �ս� �Լ�
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent4() //"2�� �Ҹ�, �ִ� ü���� n% �Ұ� ��� ���� ȹ��"
    {
        curDay += 2;

        cur_HP -= max_HP / 10; // 10% ���� ����
        if (cur_HP < 1) cur_HP = 1;
        // �ִ�ü���� n% �Ҵ� �Լ�, ���� ���� ü���� n%���� �۴ٸ� ���� ü���� 1�� ����
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent5() //"3�� �Ҹ�, �ִ� ü���� n% + n��� �Ұ� ���� ���� ȹ��"
    {
        curDay += 3;
        cur_HP -= max_HP / 10;
        if (cur_HP < 1) cur_HP = 1;
        // �ִ�ü���� n% �Ҵ� �Լ�, ���� ���� ü���� n%���� �۴ٸ� ���� ü���� 1�� ����
        curGold -= 50; // ��� �ս� �Լ�
        // relicManager.GetRelic(eventNode.SetLegRel(kingdom));

        ButtonsOff();
    }

    void ActiveEvent6() //"1�� �Ҹ�, ��带 ��� �Ұ� ��� ���� ȹ��"
    {
        curDay += 1;
        curGold = 0; // ��� �ս� �Լ�
        // relicManager.GetRelic(eventNode.SetEpicRel(kingdom));


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
