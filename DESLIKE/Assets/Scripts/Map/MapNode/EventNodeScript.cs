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

    void VarChange()    // ���� ������(��Ʋ��� -> ��Ʋ��� ��ũ��Ʈ)
    {
        for (int i = 0; i < 3; i++)
            isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
        reward = eventNode.reward;
        soldierReward = eventNode.soldierReward;
        ableSoldierRewards =eventNode.ableSoldierRewards;
        ableRelicRewards = eventNode.ableRelicRewards;
        map = eventNode.map;
    }

    void SetEventNodeData()   // �ҷ�����
    {
        for (int i = 0; i < 3; i++)
        {
            eventNode.isRewardSet = saveManager.gameData.mapData.isRewardSet[i];   // ��Ʋ ��� ����
            isRewardSet = eventNode.isRewardSet;   // ��Ʋ ��� ��ũ��Ʈ��
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
                reward.soldierReward.Add(soldierReward);   // ���� ������ �߰�
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            // reward.relic = ableRelicRewards[relicRewardIndex[0]];  // ���� �ҷ�����
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
                reward.soldierReward.Add(soldierReward);   // ���� ������ �߰�
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[1];
            // reward.relic = ableRelicRewards[relicRewardIndex[1]];  // ���� �ҷ�����
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
                reward.soldierReward.Add(soldierReward);   // ���� ������ �߰�
            }
            // relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            // reward.relic = ableRelicRewards[relicRewardIndex[0]];  // ���� �ҷ�����
            */
        }

        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet = true;
    }

    public void NorSolSet(int num, int button)  // �Ϲ� ���� 1���� �߰�
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // ������ + ����
        else norTotal = speNorSolC + comNorSolC; // �ּ��� + ����

        // randomInt:
        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� ������ ������ ����
        // if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
        //    goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�
        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        // saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
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

        // randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        // if (num == 1 && (eventNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
        //     goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        soldierReward.soldier = eventNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�
        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        // saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = eventNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
    }

    public void NorRelSet(int button)   // �Ϲ� ���� ����
    {
        int norTotal;
        if (eventNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // ������ + ����
        else
            norTotal = speNorRelC + comNorRelC;  // �ּ��� + ����

        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� �� ������
        reward.relic = ableRelicRewards[rand];  // �ش� ������ ��忡 ����
        // saveManager.gameData.curBattleNodeData.relRewardIndex[button, 0] = rand;    // ���� ��ȣ ���ӵ����Ϳ� ����
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

        reward.relic = ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
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
        reward.relic = ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }


    public void Play_EventNode()
    {
        eventNode.Play_EventNode();
    }
 }
