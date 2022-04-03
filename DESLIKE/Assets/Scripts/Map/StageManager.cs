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
        curStage = saveManager.gameData.mapData.curStage;
        StageInitialize();  // �������� �ʱ�ȭ(SetActive(false))
        StageSetting();
    }

    void StageInitialize()
    {
        for (int i = 0; i < 3; i++)
            Stage[i].gameObject.SetActive(false);
    }

    void StageSetting() // �������� Ȱ��ȭ
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
