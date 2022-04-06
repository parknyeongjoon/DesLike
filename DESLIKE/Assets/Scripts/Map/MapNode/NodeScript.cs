using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;
    protected Image nodeImg;
    public MapNode MapNode { get => mapNode; set => mapNode = value; }
    SaveManager saveManager;
    SoldierReward soldierReward = new SoldierReward();
    Kingdom kingdom;
    public bool[] isRewardSet = new bool[3];
    int[] soldierRewardIndex1 = new int[3];
    int[] soldierRewardIndex2 = new int[3];
    int[] relicRewardIndex = new int[3];

    int phyNorSolC, speNorSolC, comNorSolC, phyEpicSolC, speEpicSolC, comEpicSolC, 
        phyNorRelC, speNorRelC, comNorRelC, phyEpicRelC, speEpicRelC, comEpicRelC, phyLegendRelC, speLegendRelC, comLegendRelC;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        kingdom = mapNode.kingdom;
        soldierReward.mutant = null;    // 일단 돌연변이는 null
        
        SetData();
    }

    void SetData()   // 불러오기
    {
        for (int i = 0; i < 3; i++)
        {
            isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];
            if (isRewardSet[i] == true) // 이미 세팅 값이 있으면 가져오기
            {
                soldierRewardIndex1[i] = saveManager.gameData.rewardData.soldierRewardIndex1[i];
                soldierRewardIndex2[i] = saveManager.gameData.rewardData.soldierRewardIndex2[i];
            }
            else // 아니면 초기화
            {
                soldierRewardIndex1[i] = 0;
                soldierRewardIndex2[i] = 0;
            }
        }

        phyNorSolC = mapNode.map.physicNorSol.Count;
        speNorSolC = mapNode.map.spellNorSol.Count;
        comNorSolC = mapNode.map.commonNorSol.Count;
        phyEpicSolC = mapNode.map.physicEpicSol.Count;
        speEpicSolC = mapNode.map.spellEpicSol.Count;
        comEpicSolC = mapNode.map.commonEpicSol.Count;

        phyNorRelC = mapNode.map.physicNorRel.Count;
        speNorRelC = mapNode.map.spellNorRel.Count;
        comNorRelC = mapNode.map.commonNorRel.Count;
        phyEpicRelC = mapNode.map.physicEpicRel.Count;
        speEpicRelC = mapNode.map.spellEpicRel.Count;
        comEpicRelC = mapNode.map.commonEpicRel.Count;
        phyLegendRelC = mapNode.map.physicLegendRel.Count;
        speLegendRelC = mapNode.map.spellLegendRel.Count;
        comLegendRelC = mapNode.map.commonLegendRel.Count;

    }

    public void SetReward1()
    {
        mapNode.SetAbleReward();
        
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
            mapNode.reward.soldierReward.Clear();

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex1[0]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex2[0]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지2
            
            relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];    
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex[0]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[0] = true;
        isRewardSet[0] = true;
    }

    public void SetReward2()
    {
        mapNode.SetAbleReward();
      
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
            mapNode.reward.soldierReward.Clear();

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex1[1]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex1[2]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지2

            relicRewardIndex[1] = saveManager.gameData.rewardData.relicRewardIndex[1];
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex[1]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[1] = true;
        isRewardSet[1] = true;
    }

    public void SetReward3()
    {
        mapNode.SetAbleReward();
     
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
            mapNode.reward.soldierReward.Clear();

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex1[2]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = mapNode.ableSoldierRewards[soldierRewardIndex1[2]];
            mapNode.reward.soldierReward.Add(soldierReward);   // 병사 선택지2

            relicRewardIndex[2] = saveManager.gameData.rewardData.relicRewardIndex[2];
            mapNode.reward.relic = mapNode.ableRelicRewards[relicRewardIndex[2]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet[2] = true;
    }

    public void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // 무투국 + 공통
        else norTotal = speNorSolC + comNorSolC; // 주술국 + 공통

        int solReward;  // 다른 선택지와 비교 위한 변수
        if (num == 0) solReward = -1;   // 첫번째 선택지일 경우 비교할 값이 없으니까 쓰레기값 설정
        else solReward = soldierRewardIndex1[button];   // 두번째 선택지면 첫번째 선택지 가져오기

        randomInt:
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        if (num == 1 && rand == solReward) goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) mapNode.reward.soldierReward.Clear();
        soldierReward.soldier = mapNode.ableSoldierRewards[rand];
        mapNode.reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        if (num == 0)
        {
            soldierRewardIndex1[button] = rand;   // 첫 선택지 저장
            saveManager.gameData.rewardData.soldierRewardIndex1[button] = rand;
        }
        else
        {
            soldierRewardIndex2[button] = rand;    // 두번째 선택지 저장
            saveManager.gameData.rewardData.soldierRewardIndex2[button] = rand;
        }
    }

    public void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
    {
        int norTotal, epicTotal;
        if (kingdom == Kingdom.Physic)
        {
            norTotal = phyNorSolC + comNorSolC;
            epicTotal = phyEpicSolC + comEpicSolC;
        }
        else
        {
            norTotal = speNorSolC + comNorSolC;
            epicTotal = speEpicSolC + comEpicSolC;
        }

        int solReward;
        if (num == 0) solReward = -1;
        else solReward = soldierRewardIndex1[button];

        randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && rand == solReward) goto randomInt;

        if (num == 0) mapNode.reward.soldierReward.Clear();
        soldierReward.soldier = mapNode.ableSoldierRewards[rand];
        mapNode.reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        if (num == 0)
        {
            soldierRewardIndex1[button] = rand;
            saveManager.gameData.rewardData.soldierRewardIndex1[button] = rand;
        }
        else
        {
            soldierRewardIndex2[button] = rand;
            saveManager.gameData.rewardData.soldierRewardIndex2[button] = rand;
        }
    }

    public void NorRelSet(int button)   // 일반 유물 설정
    {
        int norTotal;
        if (kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // 무투국 + 공통
        else
            norTotal = speNorRelC + comNorRelC;  // 주술국 + 공통

        int rand = Random.Range(0, norTotal);   // 일반 범위 내 랜덤값
        mapNode.reward.relic = mapNode.ableRelicRewards[rand];  // 해당 유물을 노드에 저장
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;    // 유물 번호 게임데이터에 저장
    }

    public void EpicRelSet(int button)  // 희귀 유물 설정, 주석은 NorRelSet 참고
    {
        int norTotal, epicTotal;
        if (kingdom == Kingdom.Physic)
        {
            norTotal = phyNorRelC + comNorRelC;
            epicTotal = phyEpicRelC + comEpicRelC;
        }
        else
        {
            norTotal = speNorRelC + comNorRelC;
            epicTotal = speEpicRelC + comEpicRelC;
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
            neTotal = phyNorRelC + comNorRelC + phyEpicRelC + comEpicRelC;
            legendTotal = phyLegendRelC + comLegendRelC;
        }
        else
        {
            neTotal = speNorRelC + comNorRelC + speEpicRelC + comEpicRelC;
            legendTotal = speLegendRelC + comLegendRelC;
        }
        int rand = Random.Range(0, legendTotal) + neTotal;
        mapNode.reward.relic = mapNode.ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }
}