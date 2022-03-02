using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleDestroy : MonoBehaviour
{
    void Awake()
    {
        GetComponent<CastleInfo>().afterDeadHandler += EndStage;
    }

    public void EndStage()
    {
        GameManager.Instance.gamePause = true;
        Time.timeScale = 0;
        BattleUIManager.Instance.SetRewardPanel();
    }
}