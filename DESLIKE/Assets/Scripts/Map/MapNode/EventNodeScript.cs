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
    const int THREE = 3;
    int[] nextEvent = new int[THREE];
    Map map;
    public bool[] isEventSet = new bool[THREE];
    bool[] isRewardSet = new bool[THREE];
    Reward reward;
    List<SoldierData> ableSoldierRewards;
    List<Relic> ableRelicRewards;
    List<Option> option = new List<Option>();

    void Start()
    {
        eventNode = (EventNode)MapNode;
        saveManager = SaveManager.Instance;
        VarChange();
        SetEventNodeData();
    }

    void VarChange()    // ���� ������(��Ʋ��� -> ��Ʋ��� ��ũ��Ʈ)
    {
        for (int i = 0; i < 3; i++)
        {
            isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
            eventNode.isEventSet[i] = isEventSet[i];
        }
        reward = eventNode.reward;
        ableSoldierRewards =eventNode.ableSoldierRewards;
        ableRelicRewards = eventNode.ableRelicRewards;
        map = eventNode.map;
    }

    void SetEventNodeData()   // �ҷ�����
    {
        for (int i = 0; i < 3; i++)
        {
            isEventSet[i] = eventNode.isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
            isRewardSet[i] = eventNode.isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];
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
            NorSolSet(0, 0);
            // EpicSolSet(0,0);
            NorSolSet(1, 0);
            // EpicSolSet(1,0)
            NorRelSet(0);
            // EpicRelSet(0);
            // LegendRelSet(0);
        }
       
        saveManager.gameData.mapData.isRewardSet[0] = true;
        isRewardSet[0] = true;
    }

    void SetEventReward2()
    {
        eventNode.SetAbleReward();
        isRewardSet[1] = saveManager.gameData.mapData.isRewardSet[1];
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
        
        saveManager.gameData.mapData.isRewardSet[1] = true;
        isRewardSet[1] = true;
    }

    void SetEventReward3()
    {
        eventNode.SetAbleReward();

        isRewardSet[2] = saveManager.gameData.mapData.isRewardSet[2];
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
       
        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet[2] = true;
    }

    public void NorSolSet(int num, int button)  // �Ϲ� ���� 1���� �߰�
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // ������ + ����
        else norTotal = speNorSolC + comNorSolC; // �ּ��� + ����

        randomInt:
        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� ������ ������ ����
        if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = eventNode.ableSoldierRewards[rand];

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�
        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
    }

    public void EpicSolSet(int num, int button)  // ��� ���� 1���� �߰�, �ּ��� NorSolSet ����
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

        randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�
        eventNode.reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
    }

    public void NorRelSet(int button)   // �Ϲ� ���� ����
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // ������ + ����
        else
            norTotal = speNorRelC + comNorRelC;  // �ּ��� + ����

        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� �� ������
        eventNode.reward.relic = eventNode.ableRelicRewards[rand];  // �ش� ������ ��忡 ����
        // saveManager.gameData.curBattleNodeData.relRewardIndex[button, 0] = rand;    // ���� ��ȣ ���ӵ����Ϳ� ����
        // �ߺ� �������� Ȯ���ؾ���
    }

    public void EpicRelSet(int button)  // ��� ���� ����, �ּ��� NorRelSet ����
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

        eventNode.reward.relic = eventNode.ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
        // �ߺ� �������� Ȯ���ؾ���
    }

    public void LegendRelSet(int button)  // ���� ���� ����, �ּ��� NorRelSet ����
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
        eventNode.reward.relic = eventNode.ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
        // �ߺ� �������� Ȯ���ؾ���
    }


    public void Play_EventNode()
    {
        for (int i = 0; i < 3; i++)
        {
            eventNode.isEventSet[i] = false;
            eventNode.isRewardSet[i] = false;
        }
        eventNode.Play_EventNode();
    }
 }
