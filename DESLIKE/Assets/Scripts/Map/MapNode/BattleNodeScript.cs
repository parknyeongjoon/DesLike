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
            isEventSet[i] = saveManager.gameData.mapData.isEventSet[i];
        reward = battleNode.reward;
        soldierReward = battleNode.soldierReward;
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
            isEventSet[i] = false;
        battleNode.isRewardSet = false;
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
    }

    public void BattleNodeSet2()
    {
        SetEnemyPort(1);
        SetBattleReward2();
    }

    public void BattleNodeSet3()
    {
        SetEnemyPort(2);
        SetBattleReward3();
    }

    void SetEnemyPort(int i)    // �� ����
    {
        SetEnemyPortOption(i);
        battleNode.isEventSet[i] = true;
        battleNode.isRewardSet = true;
    }

    
    // -------------
    void SetBattleNodeData()   // �ҷ�����
    {
        for (int i = 0; i < 3; i++)
        {
            battleNode.isRewardSet = saveManager.gameData.mapData.isRewardSet[i];   // ��Ʋ ��� ����
            isRewardSet = battleNode.isRewardSet;   // ��Ʋ ��� ��ũ��Ʈ��
            if (isRewardSet == true) // �̹� ���� ���� ������ ��������
            {
                soldierRewardIndex[0, i] = saveManager.gameData.rewardData.soldierRewardIndex[0, i];
                soldierRewardIndex[1, i] = saveManager.gameData.rewardData.soldierRewardIndex[1, i];
            }
            else // �ƴϸ� �ʱ�ȭ
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

    public void SetBattleReward1()
    {
        battleNode.SetAbleReward();

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
            reward.soldierReward.Add(soldierReward);   // ���� ������1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 0]];
            reward.soldierReward.Add(soldierReward);   // ���� ������2

            relicRewardIndex[0] = saveManager.gameData.rewardData.relicRewardIndex[0];
            reward.relic = ableRelicRewards[relicRewardIndex[0]];  // ���� �ҷ�����
        }
        saveManager.gameData.mapData.isRewardSet[0] = true;
        isRewardSet = true;
    }

    public void SetBattleReward2()
    {
        battleNode.SetAbleReward();

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
            reward.soldierReward.Add(soldierReward);   // ���� ������1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 1]];
            reward.soldierReward.Add(soldierReward);   // ���� ������2

            relicRewardIndex[1] = saveManager.gameData.rewardData.relicRewardIndex[1];
            reward.relic = ableRelicRewards[relicRewardIndex[1]];  // ���� �ҷ�����
        }
        saveManager.gameData.mapData.isRewardSet[1] = true;
        isRewardSet = true;
    }

    public void SetBattleReward3()
    {
        battleNode.SetAbleReward();

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
            reward.soldierReward.Add(soldierReward);   // ���� ������1

            soldierReward.soldier = ableSoldierRewards[soldierRewardIndex[1, 2]];
            reward.soldierReward.Add(soldierReward);   // ���� ������2

            relicRewardIndex[2] = saveManager.gameData.rewardData.relicRewardIndex[2];
            reward.relic = ableRelicRewards[relicRewardIndex[2]];  // ���� �ҷ�����
        }
        saveManager.gameData.mapData.isRewardSet[2] = true;
        isRewardSet = true;
    }

    public void NorSolSet(int num, int button)  // �Ϲ� ���� 1���� �߰�
    {
        int norTotal;
        if (battleNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // ������ + ����
        else norTotal = speNorSolC + comNorSolC; // �ּ��� + ����

        int solReward;  // �ٸ� �������� �� ���� ����
        if (num == 0) solReward = -1;   // ù��° �������� ��� ���� ���� �����ϱ� �����Ⱚ ����
        else solReward = soldierRewardIndex[0, button];   // �ι�° �������� ù��° ������ ��������

        randomInt:
        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� ������ ������ ����
        if (num == 1 && rand == solReward) goto randomInt;  // �ٸ� �������� �ߺ��̸� �ٽ� �̱�

        if (num == 0) reward.soldierReward.Clear();
        soldierReward.soldier = ableSoldierRewards[rand];
        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

        if (num == 0)
        {
            soldierRewardIndex[0, button] = rand;   // ù ������ ����
            saveManager.gameData.rewardData.soldierRewardIndex[0, button] = rand;
        }
        else
        {
            soldierRewardIndex[1, button] = rand;    // �ι�° ������ ����
            saveManager.gameData.rewardData.soldierRewardIndex[1, button] = rand;
        }
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

        int solReward;
        if (num == 0) solReward = -1;
        else solReward = soldierRewardIndex[0, button];

        randomInt:
        int rand = norTotal + Random.Range(0, epicTotal);
        if (num == 1 && rand == solReward) goto randomInt;

        if (num == 0) reward.soldierReward.Clear();
        soldierReward.soldier = ableSoldierRewards[rand];
        reward.soldierReward.Add(soldierReward);   // �������� ���� �߰�

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

    public void NorRelSet(int button)   // �Ϲ� ���� ����
    {
        int norTotal;
        if (battleNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // ������ + ����
        else
            norTotal = speNorRelC + comNorRelC;  // �ּ��� + ����

        int rand = Random.Range(0, norTotal);   // �Ϲ� ���� �� ������
        reward.relic = ableRelicRewards[rand];  // �ش� ������ ��忡 ����
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;    // ���� ��ȣ ���ӵ����Ϳ� ����
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

        reward.relic = ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
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
        reward.relic = ableRelicRewards[rand];
        saveManager.gameData.rewardData.relicRewardIndex[button] = rand;
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

    public void See_InfoPanel()
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
