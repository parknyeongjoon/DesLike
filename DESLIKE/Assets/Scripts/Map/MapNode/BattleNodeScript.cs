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

    void VarChange()    // 변수 재정의(배틀노드 -> 배틀노드 스크립트)
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

    void SetEnemyPort(int i)    // 적 세팅
    {
        SetEnemyPortOption(i);
        battleNode.isEventSet[i] = true;
        battleNode.isRewardSet[i] = true;
        See_InfoPanel();
    }
    
    // -------------
    void SetBattleNodeData()   // 불러오기
    {
        for (int i = 0; i < 3; i++)
        {
            // if (i < 2) soldierRewardNum[i] = battleNode.soldierRewardNum[i];
            battleNode.isRewardSet[i] = saveManager.gameData.mapData.isRewardSet[i];   // 배틀 노드 저장
        }
        isRewardSet = battleNode.isRewardSet;   // 배틀 노드 스크립트용
        
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

    public void NorSolSet(int num, int button)  // 일반 병사 1마리 추가
    {
        int norTotal;
        if (battleNode.kingdom == Kingdom.Physic) norTotal = phyNorSolC + comNorSolC;  // 무투국 + 공통
        else norTotal = speNorSolC + comNorSolC; // 주술국 + 공통

        randomInt:
        int rand = Random.Range(0, norTotal);   // 일반 범위 내에서 랜덤값 설정
        if (num == 1 && (battleNode.ableSoldierRewards[rand].code == saveManager.gameData.curBattleNodeData.solRewardIndex[button, 0]))
            goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = battleNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요

        reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = battleNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }

    public void EpicSolSet(int num, int button)  // 희귀 병사 1마리 추가, 주석은 NorSolSet 참고
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
            goto randomInt;  // 다른 선택지와 중복이면 다시 뽑기

        if (num == 0) reward.soldierReward.Clear();

        SoldierReward soldierReward = new SoldierReward();
        soldierReward.soldier = battleNode.ableSoldierRewards[rand];
        // soldierReward.mutant = ...; // mutant 추가 필요
        battleNode.reward.soldierReward.Add(soldierReward);   // 선택지에 병사 추가

        saveManager.gameData.curBattleNodeData.solRewardIndex[button, num] = battleNode.ableSoldierRewards[rand].code;  // 병사 코드 저장
    }

    public void NorRelSet(int button)   // 일반 유물 설정
    {
        int norTotal;
        if (battleNode.kingdom == Kingdom.Physic)
            norTotal = phyNorRelC + comNorRelC; // 무투국 + 공통
        else norTotal = speNorRelC + comNorRelC;  // 주술국 + 공통

        int rand = Random.Range(0, norTotal);   // 일반 범위 내 랜덤값
        battleNode.reward.relic = battleNode.ableRelicRewards[rand];  // 해당 유물을 노드에 저장
        // saveManager.gameData.curBattleNodeData.relRewardIndex[button, 0] = rand;    // 유물 번호 게임데이터에 저장
    }

    public void EpicRelSet(int button)  // 희귀 유물 설정, 주석은 NorRelSet 참고
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

    public void LegendRelSet(int button)  // 전설 유물 설정, 주석은 NorRelSet 참고
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
        if (isEventSet[i] == false)    // 새로운 정보 세팅
        {
            int temp = Random.Range(0, enemyPortsOptions.Count);
            battleNode.enemyPortOption = enemyPortsOptions[temp];
            nextEvent[i] = temp;
        }
        else   // 기존 정보 가져오기
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
        for (int i = 0; i < option.Count; i++) // 주요 병력 3개만 보여주기
        {
            // 병력 전투력순 정렬 필요
            // 주요 병력 3개 고르기
            if (i < 3)
            {
                createPrefab = Instantiate(InfoPrefab, InfoTemp.transform);
                createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
                createPrefab.GetComponentInChildren<TMP_Text>().text = "총 유닛 포트 : "
                    + option[i].portNum.Length.ToString() + "포트";
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
        for (int i = 0; i < option.Count; i++)  // 병력 전체 정렬
        {
            createPrefab = Instantiate(MorePrefab, MoreTemp.transform);
            createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
            createPrefab.GetComponentInChildren<TMP_Text>().text = option[i].portNum.Length.ToString();
        }
    }
}
