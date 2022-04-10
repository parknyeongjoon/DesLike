using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeScript : NodeScript
{
    EventNode eventNode;
    [SerializeField] GameObject InfoTemp;
    SaveManager saveManager;
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
            /*
            reward.soldierReward.Clear();
            for (int i = 0; i < 2; i++)
            {
                soldierReward.soldier = saveManager.dataSheet.soldierDataSheet[saveManager.gameData.curBattleNodeData.solRewardIndex[0, i]];
                reward.soldierReward.Add(soldierReward);   // 병사 선택지 추가
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            // reward.relic = ableRelicRewards[relicRewardIndex[0]];  // 유물 불러오기
            */
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
            /*
            reward.soldierReward.Clear();
            for (int i = 0; i < 2; i++)
            {
                soldierReward.soldier = saveManager.dataSheet.soldierDataSheet[saveManager.gameData.curBattleNodeData.solRewardIndex[1, i]];
                reward.soldierReward.Add(soldierReward);   // 병사 선택지 추가
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[1];
            // reward.relic = ableRelicRewards[relicRewardIndex[1]];  // 유물 불러오기
            */
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
            /*
            reward.soldierReward.Clear();
            for (int i = 0; i < 2; i++)
            {
                soldierReward.soldier = saveManager.dataSheet.soldierDataSheet[saveManager.gameData.curBattleNodeData.solRewardIndex[2, i]];
                reward.soldierReward.Add(soldierReward);   // 병사 선택지 추가
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            // reward.relic = ableRelicRewards[relicRewardIndex[0]];  // 유물 불러오기
            */
        }

        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet = true;
    }

    public void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // 무투국 + 공통
        else norTotal = speNorSolC + comNorSolC; // 주술국 + 공통

        // randomInt:
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        // if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
        //    goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요
        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        // saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }

    public void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
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

        // randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        // if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
        //     goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요
        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        // saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }

    public void NorRelSet(int button)   // 일반 유물 설정
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // 무투국 + 공통
        else
            norTotal = speNorRelC + comNorRelC;  // 주술국 + 공통

        int rand = Random.Range(0, norTotal);   // 일반 범위 내 랜덤값
        reward.relic = ableRelicRewards[rand];  // 해당 유물을 노드에 저장
        // saveManager.gameData.curBattleNodeData.relRewardIndex[button, 0] = rand;    // 유물 번호 게임데이터에 저장
    }

    public void EpicRelSet(int button)  // 희귀 유물 설정, 주석은 NorRelSet 참고
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
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    public void LegendRelSet(int button)  // 전설 유물 설정, 주석은 NorRelSet 참고
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
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }


    public void Play_EventNode()
    {
        eventNode.Play_EventNode();
    }
 }
