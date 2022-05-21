using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventNodeScript : NodeScript
{
    EventNode eventNode;
    [SerializeField] GameObject InfoTemp;
    SaveManager saveManager;
    int phyNorSolC, speNorSolC, phyEpicSolC, speEpicSolC;
    const int THREE = 3;
    int[] nextEvent = new int[THREE];
    public bool[] isEventSet = new bool[THREE];
    bool[] isRewardSet = new bool[THREE];
    Reward reward;
    List<SoldierData> ableSoldierRewards;
    List<Option> option = new List<Option>();
    Map map;

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
        {
            isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
            eventNode.isEventSet[i] = isEventSet[i];
        }
        reward = eventNode.reward;
        ableSoldierRewards =eventNode.ableSoldierRewards;
        map = eventNode.map;
    }

    void SetEventNodeData()   // 불러오기
    {
        for (int i = 0; i < 3; i++)
        {
            isEventSet[i] = eventNode.isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
            isRewardSet[i] = eventNode.isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];
        }

        phyNorSolC = map.physicNorSol.Count;
        speNorSolC = map.spellNorSol.Count;
        phyEpicSolC = map.physicEpicSol.Count;
        speEpicSolC = map.spellEpicSol.Count;
    }

    public void EventNodeSet1()
    {
        SetEventReward1();
        GameManager.DeleteChilds(InfoTemp);
        map.selectNode[0] = this.eventNode;
    }

    public void EventNodeSet2()
    {
        SetEventReward2();
        GameManager.DeleteChilds(InfoTemp);
        map.selectNode[1] = this.eventNode;
    }

    public void EventNodeSet3()
    {
        SetEventReward3();
        GameManager.DeleteChilds(InfoTemp);
        map.selectNode[2] = this.eventNode;
    }

    void SetEventReward1()
    {
        eventNode.SetAbleReward();
        isRewardSet[0] = saveManager.gameData.mapData.isRewardSet[0];
        if (isRewardSet[0] == false)
        {
            eventNode.reward.relicReward.Clear();
            NorSolSet(0, 0);
            // EpicSolSet(0,0);
            NorSolSet(1, 0);
            // EpicSolSet(1,0)
            eventNode.SetNorRel();
        }
        isRewardSet[0] = saveManager.gameData.mapData.isRewardSet[0] = true;
    }

    void SetEventReward2()
    {
        eventNode.SetAbleReward();
        isRewardSet[1] = saveManager.gameData.mapData.isRewardSet[1];
        if (isRewardSet[1] == false)
        {
            eventNode.reward.relicReward.Clear();
            NorSolSet(0, 1);
            // EpicSolSet(0,1);
            NorSolSet(1, 1);
            // EpicSolSet(1,1)
            eventNode.SetNorRel();
        }
        isRewardSet[1] = saveManager.gameData.mapData.isRewardSet[1] = true;
        
    }

    void SetEventReward3()
    {
        eventNode.SetAbleReward();

        isRewardSet[2] = saveManager.gameData.mapData.isRewardSet[2];
        if (isRewardSet[2] == false)
        {
            eventNode.reward.relicReward.Clear();
            NorSolSet(0, 2);
            // EpicSolSet(0,2);
            NorSolSet(1, 2);
            // EpicSolSet(1,2)
            eventNode.SetNorRel();
        }

        isRewardSet[2] = saveManager.gameData.mapData.isRewardSet[2] = true;
    }

    public void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC;  // 무투국 + 공통
        else norTotal = speNorSolC; // 주술국 + 공통

        randomInt:
        InfiniteLoopDetector.Run();
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = eventNode.ableSoldierRewards[rand];

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요
        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }

    public void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
    {
        int norTotal, epicTotal;
        if (eventNode.kingdom == Kingdom.Physic)
        {
            norTotal = phyNorSolC;
            epicTotal = phyEpicSolC;
        }
        else
        {
            norTotal = speNorSolC;
            epicTotal = speEpicSolC;
        }

        randomInt:
        InfiniteLoopDetector.Run();
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요
        eventNode.reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }
    
    public void Play_EventNode()
    {
  
        for (int i = 0; i < 3; i++)
        {
            saveManager.gameData.mapData.isEventSet[i] = false;
            saveManager.gameData.mapData.isRewardSet[i] = false;
        }

        saveManager.gameData.mapData.eventEnd = true;
        saveManager.SaveGameData();

        eventNode.Play_EventNode();
    }
}
