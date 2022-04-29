using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaEvent : MonoBehaviour
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
    int[] areaGold = new int[3];
    

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
            EventOption1();
            EventOption2();
            EventOption3();
        }
    }

    void EventOption1()    // 1�� ����
    {
        areaGold[0] = 40 + Random.Range(0, 20);
        OptionText[0].text = "1�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
    }

    void EventOption2()    // 2�� ����
    {
        areaGold[1] = 70 + Random.Range(0, 30);
        OptionText[1].text = "2�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
    }

    void EventOption3()    // 3�� ����
    {
        areaGold[2] = 100 + Random.Range(0, 40);
        OptionText[2].text = "3�� �Ҹ�, ����ȭ��" + areaGold + "ȹ��";
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
        saveManager.gameData.mapData.curDay += 1;
        Debug.Log("��ư1");
        saveManager.gameData.goodsSaveData.areaGold += areaGold[0];
        ButtonsOff();
    }

    public void Button2()
    {
        saveManager.gameData.mapData.curDay += 2;
        saveManager.gameData.goodsSaveData.areaGold += areaGold[1];
        ButtonsOff();
    }

    public void Button3()
    {
        saveManager.gameData.mapData.curDay += 3;
        saveManager.gameData.goodsSaveData.areaGold += areaGold[2];
        ButtonsOff();
    }

    public void EndEvent()
    {
        SceneManager.LoadScene("Map");
    }
}
