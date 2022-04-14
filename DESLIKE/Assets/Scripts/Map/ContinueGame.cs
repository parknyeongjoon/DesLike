using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ContinueGame : MonoBehaviour
{
    SaveManager saveManager;

    public void Continue()//enum으로 바꾸기
    {
        saveManager = SaveManager.Instance;
        CurWindow curWindow = saveManager.gameData.mapData.curWindow;
        if (curWindow == CurWindow.Map)
            saveManager.gameData.mapData.newSet = false;
        else saveManager.gameData.mapData.newSet = true;

        if (curWindow == CurWindow.Map)
            SceneManager.LoadScene("Map");
        else if(curWindow == CurWindow.Event)
            SceneManager.LoadScene("Event");
        else if(curWindow == CurWindow.Battle)
            SceneManager.LoadScene("BattleField");
        else if (curWindow == CurWindow.Village)
            SceneManager.LoadScene("Village");
        else if (curWindow == CurWindow.Organ)
            SceneManager.LoadScene("Organ");
    }

    public void FromFirst() // 데이터 초기화
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData = new MapData();
        saveManager.gameData.mapData.newSet = true;
        saveManager.gameData.curBattleNodeData = new CurBattleNodeData();
    }
}
