using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaEvent : EventBasic
{
    int areaGold;
    int[] areaGolds = new int[3];
    bool isEventSet, isAlreadySelect;

    [SerializeField] Button[] Buttons = new Button[3];
    [SerializeField] TMP_Text[] OptionText = new TMP_Text[3];
   

    void OnEnable()
    {
        LoadData();
        SetOption();
        isEventSet = true;
        eventEnd = false;
        SaveData();
    }

    void SaveData()
    {
        SaveAreaEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SaveAreaEData()
    {
        saveManager.gameData.eventData.isEventSet = isEventSet;
        saveManager.gameData.eventData.isAlreadySelect = isAlreadySelect;
        for (int i = 0; i < 3; i++)
            saveManager.gameData.eventData.areaGold[i] = areaGolds[i];
    }

    void LoadData()
    {
        LoadAreaEData();
        LoadComData();
    }

    void LoadAreaEData()
    {
        isEventSet = saveManager.gameData.eventData.isEventSet;
        isAlreadySelect = saveManager.gameData.eventData.isAlreadySelect;

        for (int i = 0; i < 3; i++)
            areaGolds[i] = saveManager.gameData.eventData.areaGold[i];
    }

    void SetOption()
    {
        if (isAlreadySelect == false)   // 이미 선택되지 않았을 때만 세팅
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

    void EventOption1()    // 1번 보기
    {
        if (isEventSet == false)
            areaGolds[0] = 40 + Random.Range(0, 20);

        areaGold = areaGolds[0];
        OptionText[0].text = "1일 소모, 진영화폐" + areaGold + "획득";

    }

    void EventOption2()    // 2번 보기
    {
        if (isEventSet == false)
            areaGolds[1] = 70 + Random.Range(0, 30);
       
        areaGold = areaGolds[1];
        OptionText[1].text = "2일 소모, 진영화폐" + areaGold + "획득";
    }

    void EventOption3()    // 3번 보기
    {
        if (isEventSet == false)
            areaGolds[2] = 100 + Random.Range(0, 40);
      
        areaGold = areaGolds[2];
        OptionText[2].text = "3일 소모, 진영화폐" + areaGold + "획득";
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
