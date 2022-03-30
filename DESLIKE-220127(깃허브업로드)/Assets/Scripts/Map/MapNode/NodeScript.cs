using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;
    protected Image nodeImg;
    bool isRewardSet;
    public MapNode MapNode { get => mapNode; set => mapNode = value; }
    SaveManager saveManager;

    void Awake()
    {
        SaveManager saveManager = SaveManager.Instance;
    }

    public void SetReward1()
    {
        isRewardSet = saveManager.gameData.map.isRewardSet[0];
        if (isRewardSet == false)
        {
            int solCount = mapNode.rewardSoldierList.Count;
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
                int relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
            }
        }
    }

    public void SetReward2()
    {
        isRewardSet = saveManager.gameData.map.isRewardSet[1];
        if (isRewardSet == false)
        {
            int solCount = mapNode.rewardSoldierList.Count;
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
                int relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
            }
        }
    }

    public void SetReward3()
    {
        isRewardSet = saveManager.gameData.map.isRewardSet[2];
        if (isRewardSet == false)
        {
            int solCount = mapNode.rewardSoldierList.Count;
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
                int relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
            }
        }
    }
}