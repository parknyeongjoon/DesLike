using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EventBasic : MonoBehaviour
{
    // public HeroInfo heroInfo;
    // public HeroData heroData;
    public Map map;
    public EventNode eventNode;
    public bool isEventSet;
    SaveManager saveManager;
    public int[] optionNum = new int[3];
    public EventData eventData = new EventData();
    public TMP_Text[] OptionText = new TMP_Text[3];
    public Button[] Buttons = new Button[3];
    public Button EndButton;
    public int curDay, curGold;
    public bool isAlreadySelect; // 이미 이벤트 선택했는지 함수
    public float cur_HP;
    
    void Awake()
    {
        saveManager = SaveManager.Instance;
        DataLoad();
    }

    void DataLoad()
    {
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        isEventSet = eventData.isEventSet;
        eventData = saveManager.gameData.eventData;
        curDay = saveManager.gameData.mapData.curDay;
        isAlreadySelect = eventData.isAlreadySelect;
        // heroData = (HeroData)heroInfo.castleData;
        curGold = saveManager.gameData.goodsSaveData.gold;
        eventNode = (EventNode)map.curMapNode;
        for (int i = 0; i < 3; i++)
            optionNum[i] = saveManager.gameData.eventData.optionNum[i];
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

    public void EndEvent()
    {
        isEventSet = false;
        saveManager.gameData.mapData.curDay = curDay;
        SceneManager.LoadScene("Map");
    }
}
