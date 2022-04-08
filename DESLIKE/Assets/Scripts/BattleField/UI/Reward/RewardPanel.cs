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
        if(SaveManager.Instance.map.curMapNode.reward.relic != null)
        {
            Instantiate(rewardRelicBtnPrefab, transform);
        }
    }
}
