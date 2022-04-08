using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierOptionBtn : MonoBehaviour
{
    [SerializeField] SoldierRewardBtn btn0, btn1;
    [SerializeField] GameObject soldierRewardPanel;

    public void SeeSoldierOption()
    {
        btn0.soldierReward = SaveManager.Instance.map.curMapNode.reward.soldierReward[0];
        btn1.soldierReward = SaveManager.Instance.map.curMapNode.reward.soldierReward[1];
        soldierRewardPanel.SetActive(true);
        Destroy(gameObject);
    }
}
