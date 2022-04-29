using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleNodeScript : NodeScript
{
    BattleNode battleNode;
    [SerializeField] GameObject InfoTemp, InfoPrefab, MoreInfoPanel, MoreTemp, MorePrefab, ChallengeO;
    SaveManager saveManager;
    const int THREE = 3;
    int phyNorSolC, speNorSolC, comNorSolC, phyEpicSolC, speEpicSolC, comEpicSolC,
        phyNorRelC, speNorRelC, comNorRelC, phyEpicRelC, speEpicRelC, comEpicRelC, phyLegendRelC, speLegendRelC, comLegendRelC;
    int[] nextEvent = new int[THREE];
    Map map;
    public bool[] isEventSet = new bool[THREE];
    bool[] isRewardSet = new bool[THREE];
    Reward reward;
    List<SoldierData> ableSoldierRewards;
    List<Relic> ableRelicRewards;
    List<PortsOption> enemyPortsOptions;
    PortsOption enemyPortOption;
    PortDatas enemyPortDatas;
    List<Option> option = new List<Option>();

    void Start()
    {
        battleNode = (BattleNode)MapNode;
        saveManager = SaveManager.Instance;
        VarChange();
        SetBattleNodeData();
    }

    void VarChange()    // ���� ������(��Ʋ��� -> ��Ʋ��� ��ũ��Ʈ)
    {
        for (int i = 0; i < 3; i++)
        {
            isEventSet[i] = battleNode.isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
            isRewardSet[i] = battleNode.isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];
        }
        reward = battleNode.reward;
        ableSoldierRewards = battleNode.ableSoldierRewards;
        ableRelicRewards = battleNode.ableRelicRewards;
        enemyPortOption = battleNode.enemyPortOption;
        enemyPortDatas = battleNode.enemyPortDatas;
        enemyPortsOptions = battleNode.enemyPortsOptions;
        map = battleNode.map;
    }

    public void Play_BattleNode()
    {
        for (int i = 0; i < 3; i++)
        {
            saveManager.gameData.mapData.isEventSet[i] = false;
            saveManager.gameData.mapData.isRewardSet[i] = false;
        }
        battleNode.Play_BattleNode();
    }

    public void Play_BossNode()
    {
        for (int i = 0; i < 3; i++)
            SaveManager.Instance.gameData.mapData.isEventSet[0] = false;
        battleNode.Play_BattleNode();
    }

    public void BattleNodeSet1()
    {
        SetEnemyPort(0);
        SetBattleReward1();
        map.selectNode[0] = this.battleNode;
        See_InfoPanel();
    }

    public void BattleNodeSet2()
    {
        SetEnemyPort(1);
        SetBattleReward2();
        map.selectNode[1] = this.battleNode;
        See_InfoPanel();
    }

    public void BattleNodeSet3()
    {
        SetEnemyPort(2);
        SetBattleReward3();
        map.selectNode[2] = this.battleNode;
        See_InfoPanel();
    }

    void SetEnemyPort(int i)    // �� ����
    {
        SetEnemyPortOption(i);
        battleNode.isEventSet[i] = true;
        battleNode.isRewardSet[i] = true;
        See_InfoPanel();
    }
    
    // -------------
    void SetBattleNodeData()   // �ҷ�����
    {
        for (int i = 0; i < 3; i++)
        {
            // if (i < 2) soldierRewardNum[i] = battleNode.soldierRewardNum[i];
            battleNode.isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];   // ��Ʋ ��� ����
        }
        isRewardSet = battleNode.isRewardSet;   // ��Ʋ ��� ��ũ��Ʈ��
        
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

    public void SetBattleReward1()
    {
        battleNode.SetAbleReward();
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

    public void SetBattleReward2()
    {
        battleNode.SetAbleReward();
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

    public void SetBattleReward3()
    {
        battleNode.SetAbleReward();
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
        if (battleNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // ������ + ����
        else norTotal = speNorSolC + comNorSolC; // �ּ��� + ����

        randomInt:
        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� ������ ������ ����
        if (num == 1 && (battleNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = battleNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�

        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = battleNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
    }

    public void EpicSolSet(int num, int button)  // ��� ���� 1���� �߰�, �ּ��� NorSolSet ����
    {
        int norTotal, epicTotal;
        if (battleNode.kingdom == Kingdom.Physic)
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
        if (num == 1 && (battleNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = battleNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant �߰� �ʿ�
        battleNode.reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = battleNode.ableSoldierRewards[rand].code;  // ���� �ڵ� ����
    }

    public void NorRelSet(int button)   // �Ϲ� ���� ����
    {
        int norTotal;
        if (battleNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // ������ + ����
        else norTotal = speNorRelC + comNorRelC;  // �ּ��� + ����

        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� �� ������
        battleNode.reward.relic = battleNode.ableRelicRewards[rand];  // �ش� ������ ��忡 ����
        // saveManager.gameData.curBattleNodeData.relRewardIndex[button, 0] = rand;    // ���� ��ȣ ���ӵ����Ϳ� ����
    }

    public void EpicRelSet(int button)  // ��� ���� ����, �ּ��� NorRelSet ����
    {
        int norTotal, epicTotal;
        if (battleNode.kingdom == Kingdom.Physic)
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

        battleNode.reward.relic = battleNode.ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    public void LegendRelSet(int button)  // ���� ���� ����, �ּ��� NorRelSet ����
    {
        int neTotal, legendTotal;
        if (battleNode.kingdom == Kingdom.Physic)
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
        battleNode.reward.relic = battleNode.ableRelicRewards[rand];
        // saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
    }

    public void SetEnemyPortOption(int i)
    {
        if (isEventSet[i] == false)    // ���ο� ���� ����
        {
            int temp = Random.Range(0, enemyPortsOptions.Count);
            battleNode.enemyPortOption = enemyPortsOptions[temp];
            nextEvent[i] = temp;
        }
        else   // ���� ���� ��������
        {
            nextEvent[i] = saveManager.gameData.mapData.nextEvent[i];
            battleNode.enemyPortOption = enemyPortsOptions[nextEvent[i]];
        }
    }

    void See_InfoPanel()
    {
        option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(InfoTemp);
        for (int i = 0; i < option.Count; i++) // �ֿ� ���� 3���� �����ֱ�
        {
            // ���� �����¼� ���� �ʿ�
            // �ֿ� ���� 3�� ����
            if (i < 3)
            {
                createPrefab = Instantiate(InfoPrefab, InfoTemp.transform);
                createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
                createPrefab.GetComponentInChildren<TMP_Text>().text = "�� ���� ��Ʈ : "
                    + option[i].portNum.Length.ToString() + "��Ʈ";
            }
        }
    }

    public void See_More()
    {
        MoreInfoPanel.SetActive(true);
        List<Option> option = new List<Option>();
        option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(MoreTemp);
        for (int i = 0; i < option.Count; i++)  // ���� ��ü ����
        {
            createPrefab = Instantiate(MorePrefab, MoreTemp.transform);
            createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
            createPrefab.GetComponentInChildren<TMP_Text>().text = option[i].portNum.Length.ToString();
        }
    }
}
