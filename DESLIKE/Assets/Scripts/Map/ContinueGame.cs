using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ContinueGame : MonoBehaviour
{
    SaveManager saveManager;
    
    public void Continue()
    {
        if (saveManager.gameData.mapData.curWindow == 0)
            saveManager.gameData.mapData.newSet = false;
        else saveManager.gameData.mapData.newSet = true;

        if (saveManager.gameData.mapData.curWindow == 0)
            SceneManager.LoadScene("Map");
        else if(saveManager.gameData.mapData.curWindow == 1)
            SceneManager.LoadScene("Event");
        else SceneManager.LoadScene("BattleField");
    }

    public void FromFirst() // ������ �ʱ�ȭ
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.challengeCount = 0;
        saveManager.gameData.mapData.curStage = 0;
        saveManager.gameData.mapData.curTrack = 0;
        saveManager.gameData.mapData.curDay = 0;  // ���� ��¥
        saveManager.gameData.mapData.curWindow = 0;   // ���� â�� ��(0), �̺�Ʈ(1), ����(2)����
        saveManager.gameData.mapData.challengeCount = 0;
        saveManager.gameData.mapData.midBossCheck1 = false;
        saveManager.gameData.mapData.midBossCheck2 = false; 
        saveManager.gameData.mapData.villageCheck = false; 
        saveManager.gameData.mapData.organCheck = false; // �߰� ����, ����, ���� ����
        saveManager.gameData.mapData.newSet = true;
    }
}
