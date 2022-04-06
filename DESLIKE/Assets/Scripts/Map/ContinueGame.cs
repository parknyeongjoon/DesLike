using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ContinueGame : MonoBehaviour
{
    SaveManager saveManager;

    public void Continue()//enum으로 바꾸기
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

    public void FromFirst() // 데이터 초기화
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.challengeCount = 0;
        saveManager.gameData.mapData.curStage = 0;
        saveManager.gameData.mapData.curTrack = 0;
        saveManager.gameData.mapData.curDay = 0;  // 현재 날짜
        saveManager.gameData.mapData.curWindow = 0;   // 현재 창이 맵(0), 이벤트(1), 전투(2)인지
        saveManager.gameData.mapData.challengeCount = 0;
        saveManager.gameData.mapData.midBossCheck1 = false;
        saveManager.gameData.mapData.midBossCheck2 = false;
        saveManager.gameData.mapData.villageCheck = false;
        saveManager.gameData.mapData.organCheck = false; // 중간 보스, 마을, 정비 여부
        saveManager.gameData.mapData.newSet = true;
        saveManager.gameData.mapData.kingdom = Kingdom.Physic;  // 임시값
        for (int i = 0; i < 3; i++)
        {
            saveManager.gameData.mapData.isEventSet[i] = false;  // 확인용 변경
            saveManager.gameData.mapData.isRewardSet[i] = false; // 확인용 변경
        }
    }
}
