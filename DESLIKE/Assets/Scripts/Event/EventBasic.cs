using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EventBasic : MonoBehaviour
{
    public Map map;
    public EventNode eventNode;
    public SaveManager saveManager;
    public Button EndButton;
    public int curDay;
    public bool eventEnd;
    
    void Awake()
    {
        saveManager = SaveManager.Instance;
    }
    
    public void LoadComData()
    {
        eventNode = (EventNode)map.curMapNode;
        eventEnd = saveManager.gameData.mapData.eventEnd;
        curDay = saveManager.gameData.mapData.curDay;
   
    }
   
    public void SaveComData()
    {
        saveManager.gameData.mapData.curDay = curDay;
        saveManager.gameData.mapData.eventEnd = eventEnd;
    }

    public void EndEvent()
    {
        saveManager.gameData.eventData = new EventData();
        saveManager.gameData.mapData.eventEnd = true;
        SceneManager.LoadScene("Map");
    }
}
