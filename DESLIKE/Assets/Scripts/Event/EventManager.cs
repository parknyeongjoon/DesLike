using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public Map map;
    [SerializeField] Canvas EventCanvas;
    [SerializeField] GameObject RelicEvent, HealEvent, AreaEvent, PenaltyEvent, ShopEvent, GambleEvent, FightEvent;

    public EventNode eventNode;
    int curBtn, evntList;
    SaveManager saveManager;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curWindow = CurWindow.Event;
        eventNode = (EventNode)map.curMapNode; 
        curBtn = saveManager.gameData.mapData.curBtn;
        evntList = saveManager.gameData.mapData.evntList[curBtn];
        EventsInactive();
        EventActive();
        saveManager.SaveGameData();
    }

    void EventsInactive()
    {
        RelicEvent.SetActive(false);
        HealEvent.SetActive(false);
        AreaEvent.SetActive(false);
        PenaltyEvent.SetActive(false);
        ShopEvent.SetActive(false);
        GambleEvent.SetActive(false);
        FightEvent.SetActive(false);
    }

    void EventActive()
    {
        switch (evntList)
        {
            case 0:
                RelicEvent.SetActive(true);
                break;
            case 1:
                HealEvent.SetActive(true);
                break;
            case 2:
                AreaEvent.SetActive(true);
                break;
            case 3:
                PenaltyEvent.SetActive(true);
                break;
            case 4:
                ShopEvent.SetActive(true);
                break;
            case 5:
                GambleEvent.SetActive(true);
                break;
            case 6:
                FightEvent.SetActive(true);
                break;
            default:
                Debug.Log("¿À·ù");
                break;
        }
    }

  

    public void AddCurDay1()
    {
        saveManager.gameData.mapData.curDay += 1;
    }

    public void AddCurDay2()
    {
        saveManager.gameData.mapData.curDay += 2;
    }

    public void AddCurDay3()
    {
        saveManager.gameData.mapData.curDay += 3;
    }

}
