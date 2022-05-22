using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField]
    GameObject rewardRelicBtnPrefab;

    public void SetRewardPanel()
    {
        // relicBtn
        if(SaveManager.Instance.gameData.mapData.curBattle != CurBattle.Normal)//노말 아니라면 유물 증정
        {
            Instantiate(rewardRelicBtnPrefab, transform);
        }
    }
}