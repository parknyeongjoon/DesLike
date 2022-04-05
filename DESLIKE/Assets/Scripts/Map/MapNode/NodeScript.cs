using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;
    protected Image nodeImg;
    bool[] isRewardSet = new bool[3];
    public MapNode MapNode { get => mapNode; set => mapNode = value; }
    SaveManager saveManager;
    Kingdom kingdom;


    void Awake()
    {
        SaveManager saveManager = SaveManager.Instance;
        kingdom = mapNode.kingdom;

        for (int i = 0; i < 3; i++)
            isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];
    }

    public void SetReward1()
    {
        mapNode.SetAbleReward();
        
        int relicRewardIndex;
        
        if (isRewardSet[0] == false)
        {
            NorSolSet(0, 0);
            // EpicSolSet(0,0);
            NorSolSet(1, 0);
            // EpicSolSet(1,0)
            NorRelSet(0);
            // EpicRelSet(0);
            // LegendRelSet(0);
        }
        else
        {
            mapNode.reward.soldierReward.soldier[0] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[0, 0]];   // 병사 선택지1
            mapNode.reward.soldierReward.soldier[1] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[1, 0]];   // 병사 선택지2
            
            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[0];    
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex];  // 유물 불러오기
        }
    }

    public void SetReward2()
    {
        mapNode.SetAbleReward();
      
        int relicRewardIndex;

        if (isRewardSet[1] == false)
        {
            NorSolSet(0, 1);
            // EpicSolSet(0,1);
            NorSolSet(1, 1);
            // EpicSolSet(1,1)
            NorRelSet(1);
            // EpicRelSet(1);
            // LegendRelSet(1);
        }
        else
        {
            mapNode.reward.soldierReward.soldier[0] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[0, 1]];   // 병사 선택지1
            mapNode.reward.soldierReward.soldier[1] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[1, 1]];   // 병사 선택지2

            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[1];
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex];  // 유물 불러오기
        }
    }

    public void SetReward3()
    {
        mapNode.SetAbleReward();
     
        int relicRewardIndex;

        if (isRewardSet[2] == false)
        {
            NorSolSet(0, 2);
            // EpicSolSet(0,2);
            NorSolSet(1, 2);
            // EpicSolSet(1,2)
            NorRelSet(2);
            // EpicRelSet(2);
            // LegendRelSet(2);
        }
        else
        {
            mapNode.reward.soldierReward.soldier[0] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[0, 2]];   // 병사 선택지1
            mapNode.reward.soldierReward.soldier[1] = mapNode.ableSoldierRewards[saveManager.gameData.rewardData.soldierRewardIndex[1, 2]];   // 병사 선택지2

            relicRewardIndex = saveManager.gameData.rewardData.relicRewardIndex[2];
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex];  // 유물 불러오기
        }
    }

    public void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (kingdom == Kingdom.Physic) norTotal = mapNode.map.physicNorSol.Count + mapNode.map.commonNorSol.Count;  // 무투국 + 공통
        else norTotal = mapNode.map.spellNorSol.Count + mapNode.map.commonNorSol.Count; // 주술국 + 공통

        int solReward;  // 다른 선택지와 비교 위한 변수
        if (num == 0) solReward = -1;   // 첫번째 선택지일 경우 비교할 값이 없으니까 쓰레기값 설정
        else solReward = saveManager.gameData.rewardData.soldierRewardIndex[0, button];   // 두번째 선택지면 첫번째 선택지 가져오기

        randomInt:
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        if (num == 1 && rand == solReward) goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        mapNode.reward.soldierReward.soldier.Add(mapNode.ableSoldierRewards[rand]);   // 선택지에 병사 추가
        if (num == 0) saveManager.gameData.rewardData.soldierRewardIndex[0, button] = rand;   // 첫 선택지 저장
        else saveManager.gameData.rewardData.soldierRewardIndex[1, button] = rand;    // 두번째 선택지 저장
    }

    public void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
    {
        int norTotal, epicTotal;
        if (kingdom == Kingdom.Physic)
        {
            norTotal = mapNode.map.physicNorSol.Count + mapNode.map.commonNorSol.Count;
            epicTotal = mapNode.map.physicEpicSol.Count + mapNode.map.commonEpicSol.Count;
        }
        else
        {
            norTotal = mapNode.map.spellNorSol.Count + mapNode.map.commonNorSol.Count;
            epicTotal = mapNode.map.spellEpicSol.Count + mapNode.map.commonEpicSol.Count;
        }

        int solReward;
        if (num == 0) solReward = -1;
        else solReward = saveManager.gameData.rewardData.soldierRewardIndex[0, button];

        randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && rand == solReward) goto randomInt;

        mapNode.reward.soldierReward.soldier.Add(mapNode.ableSoldierRewards[rand]);
        if (num == 0) saveManager.gameData.rewardData.soldierRewardIndex[0, button] = rand;
        else saveManager.gameData.rewardData.soldierRewardIndex[1, button] = rand;
    }

    public void NorRelSet(int button)   // 일반 유물 설정
    {
        int norTotal;
        if (kingdom == Kingdom.Physic)
            norTotal = mapNode.map.physicNorRel.Count + mapNode.map.commonNorRel.Count; // 무투국 + 공통
        else
            norTotal = mapNode.map.spellNorRel.Count + mapNode.map.commonNorRel.Count;  // 주술국 + 공통

        int rand = Random.Range(0, norTotal);   // 일반 범위 내 랜덤값
        mapNode.reward.relic = mapNode.ableRelicRewards[rand];  // 해당 유물을 노드에 저장
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;    // 유물 번호 게임데이터에 저장
    }

    public void EpicRelSet(int button)  // 희귀 유물 설정, 주석은 NorRelSet 참고
    {
        int norTotal, epicTotal;
        if (kingdom == Kingdom.Physic)
        {
            norTotal = mapNode.map.physicNorRel.Count + mapNode.map.commonNorRel.Count;
            epicTotal = mapNode.map.physicEpicRel.Count + mapNode.map.commonEpicRel.Count;
        }
        else
        {
            norTotal = mapNode.map.spellNorRel.Count + mapNode.map.commonNorRel.Count;
            epicTotal = mapNode.map.spellEpicRel.Count + mapNode.map.commonEpicRel.Count;
        }
        int rand = Random.Range(0, epicTotal) + norTotal;
        mapNode.reward.relic = mapNode.ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    public void LegendRelSet(int button)  // 전설 유물 설정, 주석은 NorRelSet 참고
    {
        int neTotal, legendTotal;
        if (kingdom == Kingdom.Physic)
        {
            neTotal = mapNode.map.physicNorRel.Count + mapNode.map.commonNorRel.Count + mapNode.map.physicEpicRel.Count + mapNode.map.commonEpicRel.Count;
            legendTotal = mapNode.map.physicLegendRel.Count + mapNode.map.commonLegendRel.Count;
        }
        else
        {
            neTotal = mapNode.map.spellNorRel.Count + mapNode.map.commonNorRel.Count + mapNode.map.spellEpicRel.Count + mapNode.map.commonEpicRel.Count;
            legendTotal = mapNode.map.spellLegendRel.Count + mapNode.map.commonLegendRel.Count;
        }
        int rand = Random.Range(0, legendTotal) + neTotal;
        mapNode.reward.relic = mapNode.ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }


}