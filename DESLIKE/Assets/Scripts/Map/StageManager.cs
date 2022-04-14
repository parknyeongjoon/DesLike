using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] Stage = new GameObject[3];
    int curStage;
    SaveManager saveManager;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        StageInitialize();  // 스테이지 초기화(SetActive(false))
        isNewStage();
        curStage = saveManager.gameData.mapData.curStage;
        StageSetting();
    }

    void StageInitialize()
    {
        for (int i = 0; i < 3; i++)
            Stage[i].gameObject.SetActive(false);
    }

    void isNewStage()
    {
        if (saveManager.gameData.mapData.curDay >= 30)
        {
            if (saveManager.gameData.mapData.organCheck == true)
            {
                if (saveManager.gameData.mapData.curBattle == CurBattle.StageBoss)
                {
                    // 필요한 데이터 임시 저장
                    int curStage = saveManager.gameData.mapData.curStage;
                    Debug.Log(curStage);
                    Kingdom kingdom = saveManager.gameData.mapData.kingdom;
                    CurWindow curWindow = saveManager.gameData.mapData.curWindow;
                    int challengeCount = saveManager.gameData.mapData.challengeCount;

                    // 초기화
                    saveManager.gameData.mapData = new MapData();
                    Debug.Log("초기화");

                    // 필요 데이터 다시 넣어주기

                    saveManager.gameData.mapData.curStage = curStage + 1;
                    Debug.Log(saveManager.gameData.mapData.curStage);
                    saveManager.gameData.mapData.kingdom = kingdom;
                    saveManager.gameData.mapData.curWindow = curWindow;
                    saveManager.gameData.mapData.challengeCount = challengeCount;
                    saveManager.gameData.mapData.newSet = true;

                    BasicUI.Instance.UpdateText();
                }
            }
        }
    }
    void StageSetting() // 스테이지 활성화
    {
        switch(curStage)
        {
            case 0:
                Stage[0].gameObject.SetActive(true);
                break;

            case 1:
                Stage[1].gameObject.SetActive(true);
                break;

            case 2:
                Stage[2].gameObject.SetActive(true);
                break;
        }
    }
}
