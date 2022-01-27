using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleDestroy : MonoBehaviour
{
    [SerializeField]
    GameObject[] gameObjects;// 게임이 끝날 때 꺼야하는 게임오브젝트들

    void Awake()
    {
        GetComponent<CastleInfo>().afterDeadHandler += EndStage;
    }

    public void EndStage()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
        BattleUIManager.Instance.SetRewardPanel();
    }
}