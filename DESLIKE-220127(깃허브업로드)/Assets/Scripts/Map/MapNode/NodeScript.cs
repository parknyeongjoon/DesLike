using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;

    protected Image nodeImg;

    public MapNode MapNode { get => mapNode; set => mapNode = value; }
    
    public void SetReward()
    {
        int soldierRewardIndex = Random.Range(0, mapNode.rewardSoldierList.Count);
        mapNode.reward.soldierReward.soldier = mapNode.rewardSoldierList[soldierRewardIndex];
        int relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
        mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
    }
}
