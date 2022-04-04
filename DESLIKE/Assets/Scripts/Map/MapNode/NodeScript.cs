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
        isRewardSet = saveManager.gameData.mapData.isRewardSet[0];
        int solCount = mapNode.rewardSoldierList.Count;
        int relicRewardIndex;
        if (isRewardSet == false)
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
                relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex]; // 유물 보상(1종류)
                saveManager.gameData.rewardData.relicRewardIndex[0] = relicRewardIndex;    // 유물 저장
            }
        }
        else
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
            }
            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[0];    // 유물 불러오기
            mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
        }
    }

    public void SetReward2()
    {
        isRewardSet = saveManager.gameData.mapData.isRewardSet[1];
        int solCount = mapNode.rewardSoldierList.Count;
        int relicRewardIndex;
        if (isRewardSet == false)
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];

                relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex]; // 유물 보상(1종류)
                saveManager.gameData.rewardData.relicRewardIndex[1] = relicRewardIndex;    // 유물 저장
            }
        }
        else
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
            }
            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[1];    // 유물 불러오기
            mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
        }
    }

    public void SetReward3()
    {
        isRewardSet = saveManager.gameData.mapData.isRewardSet[2];
        int solCount = mapNode.rewardSoldierList.Count;
        int relicRewardIndex;
        if (isRewardSet == false)
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];

                relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
                mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex]; // 유물 보상(1종류)
                saveManager.gameData.rewardData.relicRewardIndex[2] = relicRewardIndex;    // 유물 저장
            }
        }
        else
        {
            for (int i = 0; i < solCount; i++)
            {
                mapNode.reward.soldierReward.soldier[i] = mapNode.rewardSoldierList[i];
            }
            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[2];    // 유물 불러오기
            mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
        }
    }
}