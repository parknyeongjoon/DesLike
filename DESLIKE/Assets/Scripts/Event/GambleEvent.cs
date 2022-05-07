using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GambleEvent : EventBasic
{
    int temp_Max_HP = 500;
    public TMP_Text InformText;
    
    void OnEnable()
    {
        SetOption();
        isEventSet = true;
        eventEnd = false;
        SaveData();
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
        InformText.text = "Ȯ���� 3���� 1, ������ 3��! ���� �� �� �غ����� �ʰڽ��ϱ�?";
        OptionText[0].text = "��带 �Ǵ�.(���� : �� ��� 3��, 3�� �Ҹ�)";
        OptionText[1].text = "ü���� �Ǵ�.(���� : �� ü���� 3�踸ŭ ȸ��, 3�� �Ҹ�)";
        OptionText[2].text = "������ ���� �ʴ´�.(�̺�Ʈ ����, 1�� �Ҹ�)";
    }

    void FirstSelect(int button)  // ��带 �Ǵ�, ü���� �Ǵ�, ���� ���Ѵ�
    {
        choiNum[0] = button;
        if (stepCheck[0] == false)
        {
            switch (button)
            {
                case 0:
                    curDay += 3;
                    InformText.text = "��ȣ, ��带 ���̱���. �󸶸� �Žðڽ��ϱ�?";
                    OptionText[0].text = "30��� (���� : 90���)";
                    OptionText[1].text = "50��� (���� : 150���)";
                    OptionText[2].text = "100��� (���� : 100���)";
                    if (curGold < 30)   // 30������ ������ ���� �� ����
                        OptionText[0].text = curGold + "��� (���� : " + curGold * 3 + "���)";
                    if (curGold < 100)
                    {
                        Buttons[2].interactable = false;    // 100��� �̸� ������ 3 ����
                        if (curGold < 50) Buttons[1].interactable = false;    // 50��� �̸� ������ 2 ����
                    }
                    break;
                case 1:
                    curDay += 3;
                    InformText.text = "��ȣ, ü���� ���̱���. �󸶳� �Žðڽ��ϱ�?";
                    OptionText[0].text = "30��� (���� : HP 90 ȸ��)";
                    OptionText[1].text = "50��� (���� : HP 150 ȸ��)";
                    OptionText[2].text = "100��� (���� : HP 100 ȸ��)";
                    if (cur_HP < 30)   // 30HP���� ������ ü�� 1����� ����
                        OptionText[0].text = (cur_HP - 1) + "HP (���� : HP " + ((cur_HP - 1) * 3) + " ȸ��)";
                    if (cur_HP < 100)
                    {
                        Buttons[2].interactable = false;    // 100 �̸� ������ 3 ����
                        if (cur_HP < 50) Buttons[1].interactable = false;    // 50 �̸� ������ 2 ����
                    }
                    break;
                case 2:
                    curDay += 1;
                    InformText.text = "ĩ, ��̾�����.�� ������.";
                    for(int i = 0; i<3; i++)
                        Buttons[i].gameObject.SetActive(false);
                    EndButton.gameObject.SetActive(true);
                    break;
                default:
                    InformText.text = "������. ���ϼ�";
                    break;
            }
        }
        stepCheck[0] = true;
        SaveStepCheck(0);
    }

    void SecondSelect(int button)   // ��� : 30 50 100 / ü�� : 30 50 100
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
                    if (choiNum[0] == 0)   // ���
                    {
                        if (curGold < 30) rewardNum = curGold;
                        else rewardNum = 30;    // 30
                        InformText.text = "��ȣ, " + rewardNum + "��带 �ẕ̇���. �� ��° ���ڸ� ���ðڽ��ϱ�?";
                    }
                    else // ü��
                    {
                        if (cur_HP <= 30) rewardNum = ((int)cur_HP - 1);
                        else rewardNum = 30;    // 30

                        InformText.text = "��ȣ, " + rewardNum + "HP�� ���̱���. �󸶸� �Žðڽ��ϱ�?";
                    }
                    break;
                case 1:
                    if (choiNum[0] == 0)   // ���
                    {
                        rewardNum = 50;
                        InformText.text = "��ȣ, " + rewardNum + "��带 �ẕ̇���. �� ��° ���ڸ� ���ðڽ��ϱ�?";
                    }
                    else
                    {
                        rewardNum = 50;
                        InformText.text = "��ȣ, " + rewardNum + "HP�� ���̱���. �󸶸� �Žðڽ��ϱ�?";
                    }
                    break;
                case 2:
                    if (choiNum[0] == 0)   // ���
                    {
                        rewardNum = 100;
                        InformText.text = "��ȣ, " + rewardNum + "��带 �ẕ̇���. �� ��° ���ڸ� ���ðڽ��ϱ�?";
                    }
                    else
                    {
                        rewardNum = 100;
                        InformText.text = "��ȣ, " + rewardNum + "HP�� ���̱���. �󸶸� �Žðڽ��ϱ�?";
                    }
                    break;
                default:
                    Debug.Log("���� ����");
                    break;
            }
        }
        comBox = Random.Range(0, 3);
        OptionText[0].text = "ù ��° ����";
        OptionText[1].text = "�� ��° ����";
        OptionText[2].text = "�� ��° ����";
        stepCheck[1] = true;
        SaveStepCheck(1);
    }

    void ThirdSelect(int button)    // 3�� �� 1�� ���߸� �� �Ϳ� ���� ���� ����, ��� : ���; ü�� : ���� Ȥ�� ü��ȸ��
    {
        choiNum[1] = button;
        // �� ��ŭ �ս�
        if (stepCheck[2] == false)
        {
            if (choiNum[0] == 0)   // �����
                curGold -= rewardNum;
            else cur_HP = rewardNum;
        }
        ToOneButton();
        InformText.text = button + "��° ���ڸ� ���̱���. ���� ����� �����մϴ�!";
        OptionText[2].text = "����..?";
        stepCheck[2] = true;
        SaveStepCheck(2);
    }

    public void Continue()
    {
        if (comBox == choiNum[1])
        {
            InformText.text = "������ " + (comBox+1) + "��° �����Դϴ�! ������ �������� ������ �帮�ڽ��ϴ�.";
            if (stepCheck[3] == true)
            {
                if (choiNum[0] == 0)
                    curGold += rewardNum * 3;
                else cur_HP += rewardNum * 3;
            }
        }
        else InformText.text = "������ " + (comBox+1) + "��° �����Դϴ�! �ƽ��Ե� Ʋ���̱���. ������ �����ϴ�.";

        for (int i = 0; i < 3; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
        stepCheck[3] = true;
        SaveStepCheck(3);
    }

    void ToOneButton()
    {
        Buttons[0].gameObject.SetActive(false);
        Buttons[1].gameObject.SetActive(false);
    }
    // ��ư�� �Լ�
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
        if (stepCheck[0] == true && choiNum[0] == 2)  // �ٷ� ���ư���.
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
