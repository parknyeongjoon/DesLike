using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeScript : NodeScript
{
    EventNode eventNode;
    [SerializeField] GameObject InfoTemp;
    SaveManager saveManager;
    int[,] soldierRewardIndex = new int[2, 3];
    int[] relicRewardIndex = new int[3];
    int phyNorSolC, speNorSolC, comNorSolC, phyEpicSolC, speEpicSolC, comEpicSolC,
        phyNorRelC, speNorRelC, comNorRelC, phyEpicRelC, speEpicRelC, comEpicRelC, phyLegendRelC, speLegendRelC, comLegendRelC;
    int[] nextEvent = new int[3];
    Map map;
    public bool[] isEventSet = new bool[3];
    bool isRewardSet;
    Reward reward;
    SoldierReward soldierReward;
    List<SoldierData> ableSoldierRewards;
    List<GameObject> ableRelicRewards;
    List<Option> option = new List<Option>();

    void Start()
    {
        eventNode = (EventNode)MapNode;
        saveManager = SaveManager.Instance;
        VarChange();
        SetEventNodeData();
    }

    void VarChange()    // 변수 재정의(배틀노드 -> 배틀노드 스크립트)
    {
        for (int i = 0; i < 3; i++)
            isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
        reward = eventNode.reward;
        soldierReward = eventNode.soldierReward;
        ableSoldierRewards =eventNode.ableSoldierRewards;
        ableRelicRewards = eventNode.ableRelicRewards;
        map = eventNode.map;
    }

    void SetEventNodeData()   // 불러오기
    {
        for (int i = 0; i < 3; i++)
        {
            eventNode.isRewardSet = saveManager.gameData.mapData.isRewardSet[i];   // 배틀 노드 저장
            isRewardSet = eventNode.isRewardSet;   // 배틀 노드 스크립트용
            if (isRewardSet == true) // 이미 세팅 값이 있으면 가져오기
            {
                soldierRewardIndex[0, i] = saveManager.gameData.rewardData.soldierRewardIndex[0, i];
                soldierRewardIndex[1, i] = saveManager.gameData.rewardData.soldierRewardIndex[1, i];
            }
            else // 아니면 초기화
            {
                soldierRewardIndex[0, i] = 0;
                soldierRewardIndex[1, i] = 0;
            }
        }

        phyNorSolC = map.physicNorSol.Count;
        speNorSolC = map.spellNorSol.Count;
        comNorSolC = map.commonNorSol.Count;
        phyEpicSolC = map.physicEpicSol.Count;
        speEpicSolC = map.spellEpicSol.Count;
        comEpicSolC = map.commonEpicSol.Count;

        phyNorRelC = map.physicNorRel.Count;
        speNorRelC = map.spellNorRel.Count;
        comNorRelC = map.commonNorRel.Count;
        phyEpicRelC = map.physicEpicRel.Count;
        speEpicRelC = map.spellEpicRel.Count;
        comEpicRelC = map.commonEpicRel.Count;
        phyLegendRelC = map.physicLegendRel.Count;
        speLegendRelC = map.spellLegendRel.Count;
        comLegendRelC = map.commonLegendRel.Count;
    }

    public void EventNodeSet1()
    {
        SetEventReward1();
        GameManager.DeleteChilds(InfoTemp);
    }

    public void EventNodeSet2()
    {
        SetEventReward2();
        GameManager.DeleteChilds(InfoTemp);
    }

    public void EventNodeSet3()
    {
        SetEventReward3();
        GameManager.DeleteChilds(InfoTemp);
    }

    void SetEventReward1()
    {
        eventNode.SetAbleReward();

        if (isRewardSet == false)
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
            reward.soldierReward.Clear();

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[0, 0]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 0]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지2

            relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            reward.relic = ableRelicRewards[relicRewardIndex[0]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[0] = true;
        isRewardSet = true;
    }

    void SetEventReward2()
    {
        eventNode.SetAbleReward();

        if (isRewardSet == false)
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
            reward.soldierReward.Clear();

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[0, 1]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 1]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지2

            relicRewardIndex[1] = saveManager.gameData.rewardData.relicRewardIndex[1];
            reward.relic = ableRelicRewards[relicRewardIndex[1]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[1] = true;
        isRewardSet = true;
    }

    void SetEventReward3()
    {
        eventNode.SetAbleReward();

        if (isRewardSet == false)
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
            reward.soldierReward.Clear();

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[0, 2]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 2]];
            reward.soldierReward.Add(soldierReward);   // 병사 선택지2

            relicRewardIndex[2] = saveManager.gameData.rewardData.relicRewardIndex[2];
            reward.relic = ableRelicRewards[relicRewardIndex[2]];  // 유물 불러오기
        }
        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet = true;
    }

    void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // 무투국 + 공통
        else norTotal = speNorSolC + comNorSolC; // 주술국 + 공통

        int solReward;  // 다른 선택지와 비교 위한 변수
        if (num == 0) solReward = -1;   // 첫번째 선택지일 경우 비교할 값이 없으니까 쓰레기값 설정
        else solReward = soldierRewardIndex[0, button];   // 두번째 선택지면 첫번째 선택지 가져오기

        randomInt:
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        if (num == 1 && rand == solReward) goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();
        soldierReward.soldier = ableSoldierRewards[rand];
        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        if (num == 0)
        {
            soldierRewardIndex[0, button] = rand;   // 첫 선택지 저장
            saveManager.gameData.rewardData.soldierRewardIndex[0, button] = rand;
        }
        else
        {
            soldierRewardIndex[1, button] = rand;    // 두번째 선택지 저장
            saveManager.gameData.rewardData.soldierRewardIndex[1, button] = rand;
        }
    }

    void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
    {
        int norTotal, epicTotal;
        if (eventNode.kingdom == Kingdom.Physic)
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
        else solReward = soldierRewardIndex[0, button];

        randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && rand == solReward) goto randomInt;

        if (num == 0) reward.soldierReward.Clear();
        soldierReward.soldier = ableSoldierRewards[rand];
        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        if (num == 0)
        {
            soldierRewardIndex[0, button] = rand;
            saveManager.gameData.rewardData.soldierRewardIndex[0, button] = rand;
        }
        else
        {
            soldierRewardIndex[1, button] = rand;
            saveManager.gameData.rewardData.soldierRewardIndex[1, button] = rand;
        }
    }

    void NorRelSet(int button)   // 일반 유물 설정
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // 무투국 + 공통
        else
            norTotal = speNorRelC + comNorRelC;  // 주술국 + 공통

        int rand = Random.Range(0, norTotal);   // 일반 범위 내 랜덤값
        reward.relic = ableRelicRewards[rand];  // 해당 유물을 노드에 저장
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;    // 유물 번호 게임데이터에 저장
    }

    void EpicRelSet(int button)  // 희귀 유물 설정, 주석은 NorRelSet 참고
    {
        int norTotal, epicTotal;
        if (eventNode.kingdom == Kingdom.Physic)
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

        reward.relic = ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    void LegendRelSet(int button)  // 전설 유물 설정, 주석은 NorRelSet 참고
    {
        int neTotal, legendTotal;
        if (eventNode.kingdom == Kingdom.Physic)
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
        reward.relic = ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    public void Play_EventNode()
    {
        eventNode.Play_EventNode();
    }
 }
