using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PenaltyEvent : EventBasic
{
    int curGold, ranPenalty;  
    // ranPenalty => 0 : ü��, 1 : ��Ʈ, 2 : Ưȭ �ս�, 3, ��� �ս�
    float cur_HP;
    bool isEventSet, isAlreadySelect;
    
    RelicManager relicManager;

    [SerializeField] Button[] Buttons = new Button[3];
    [SerializeField] TMP_Text[] OptionText = new TMP_Text[3];


    void OnEnable()
    {
        relicManager = RelicManager.Instance;

        LoadData();
        SetOption();
        isEventSet = true;
        eventEnd = false;
        SaveData();
    }

    void SaveData()
    {
        SavePenEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SavePenEData()
    {
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.ranPenalty = ranPenalty;
        saveManager.gameData.heroSaveData.cur_Hp = cur_HP;
        saveManager.gameData.eventData.isEventSet = isEventSet;
        saveManager.gameData.eventData.isAlreadySelect = isAlreadySelect;
    }

    void LoadData()
    {
        LoadPenEData();
        LoadComData();
    }

    void LoadPenEData()
    {
        curGold = saveManager.gameData.goodsSaveData.gold;
        ranPenalty = saveManager.gameData.eventData.ranPenalty;
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        isEventSet = saveManager.gameData.eventData.isEventSet;
        isAlreadySelect = saveManager.gameData.eventData.isAlreadySelect;
    }

    void SetOption()
    {
        if (isAlreadySelect == false)   // �̹� ���õ��� �ʾ��� ���� ����
        {
            if (isEventSet == false)
            {
                ranPenalty = Random.Range(0, 4);
            }
            // �ƴ϶�� ���� ����Ǿ��ִ� �����ͷ� ����

            switch (ranPenalty)
            {
                case 0: // ü��
                    OptionText[0].text = "1�� �Ҹ�, ü�� 30% �ս�";
                    OptionText[1].text = "2�� �Ҹ�, ü�� 20% �ս�";
                    OptionText[2].text = "3�� �Ҹ�, ü�� 10% �ս�";
                    break;

                case 1: // ��Ʈ
                    OptionText[0].text = "1�� �Ҹ�, ��Ʈ 1ĭ �ս�";
                    OptionText[1].text = "2�� �Ҹ�, 50% Ȯ���� ��Ʈ �ս�";
                    OptionText[2].text = "3�� �Ҹ�, ��Ʈ �ս�X";
                    break;

                case 2: // Ưȭ �ս�
                    OptionText[0].text = "1�� �Ҹ�, Ưȭ 1���� Ȯ�� �ս�";
                    OptionText[1].text = "2�� �Ҹ�, 50%Ȯ���� Ưȭ 1���� �ս�";
                    OptionText[2].text = "3�� �Ҹ�, Ưȭ �ս� X";
                    break;

                case 3: // ��� �ս�
                    OptionText[0].text = "1�� �Ҹ�, 70��� �ս�";
                    OptionText[1].text = "2�� �Ҹ�, 50��� �ս�";
                    OptionText[2].text = "3�� �Ҹ�, 30��� �ս�";
                    break;

                default:
                    OptionText[0].text = "���� ����";
                    OptionText[1].text = "���ض�";
                    OptionText[2].text = "�����";
                    break;
            }
        }
        else
        {
            ButtonsOff();
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
                Debug.Log("HP�г�Ƽ ����. ���ض� �����");
                break;
        }
    }

    void PortPenalty(int button)
    {
        switch (button)
        {
            case 0:
                // ��Ʈ �г�Ƽ
                break;

            case 1:
                int portRand = Random.Range(0, 2);
                // portRand �̿��ؼ� ��Ʈ �г�Ƽ
                break;

            case 2:// ��Ʈ �г�Ƽ X
                break;

            default:
                Debug.Log("��Ʈ�г�Ƽ ����. ���ض� �����");
                break;
        }
    }

    void MutantPenalty(int button)
    {
        switch (button)
        {
            case 0:
                // Ưȭ �г�Ƽ
                break;

            case 1:
                int mutRand = Random.Range(0, 2);
                // 50% Ȯ���� Ưȭ�г�Ƽ
                break;

            case 2:// �г�ƼX
                break;

            default:
                Debug.Log("Ưȭ�г�Ƽ ����. ���ض� �����");
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
                Debug.Log("����г�Ƽ ����. ���ض� �����");
                break;
        }
    }

    void ButtonsOff()
    {
        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
    }

    
    // ��ư�� �Լ�
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
                Debug.Log("��ư1 ����. ���ض� �����");
                break;
        }
        ButtonsOff();
        isAlreadySelect = true;
        SaveData();
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
                Debug.Log("��ư2 ����. ���ض� �����");
                break;
        }
        ButtonsOff();
        isAlreadySelect = true;
        SaveData();
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
                Debug.Log("��ư3 ����. ���ض� �����");
                break;
        }
        ButtonsOff();
        isAlreadySelect = true;
        SaveData();
    }
}
