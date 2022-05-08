using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaEvent : EventBasic
{
    void OnEnable()
    {
        SetOption();
        isEventSet = true;
        eventEnd = false;
        SaveData();
    }
  
    void SetOption()
    {
        if (isAlreadySelect == false)   // �̹� ���õ��� �ʾ��� ���� ����
        {
            for(int i = 0; i<3; i++)
                Buttons[i].gameObject.SetActive(true);
            EndButton.gameObject.SetActive(false);

            EventOption1();
            EventOption2();
            EventOption3();
        }
        else
        {
            for (int i = 0; i < 3; i++)
                Buttons[i].gameObject.SetActive(false);
            EndButton.gameObject.SetActive(true);
        }
    }

    void EventOption1()    // 1�� ����
    {
        if (isEventSet == false)
        {
            areaGolds[0] = 40 + Random.Range(0, 20);
            OptionText[0].text = "1�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
        }
        else
        {
            // ���� ������ ����
            OptionText[0].text = "1�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
        }
    }

    void EventOption2()    // 2�� ����
    {
        if (isEventSet == false)
        {
            areaGolds[1] = 70 + Random.Range(0, 30);
            OptionText[1].text = "2�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
        }
        else
        {
            OptionText[1].text = "2�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
        }
    }

    void EventOption3()    // 3�� ����
    {
        if (isEventSet == false)
        {
            areaGolds[2] = 100 + Random.Range(0, 40);
            OptionText[2].text = "3�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
        }
        else
        {
            OptionText[2].text = "3�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
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
        areaGold += areaGolds[0];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }

    public void Button2()
    {
        curDay += 2;
        areaGold += areaGolds[1];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }

    public void Button3()
    {
        curDay += 3;
        areaGold += areaGolds[2];
        isAlreadySelect = true;
        SaveData();
        ButtonsOff();
    }
}
