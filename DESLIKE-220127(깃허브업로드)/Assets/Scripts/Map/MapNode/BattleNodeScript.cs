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
    
    void Start()
    {
        saveManager = SaveManager.Instance;
        battleNode = (BattleNode)MapNode;
        battleNode.SetChallengeOption();
    }

    public void SetEnemyPort1()  // 외부 참조용
    {
        battleNode.SetEnemyPortOption(1);
        saveManager.gameData.map.isEventSet[0] = true;
        saveManager.gameData.map.isRewardSet[0] = true;
    }

    public void SetEnemyPort2()  // 외부 참조용
    {
        battleNode.SetEnemyPortOption(2);
        saveManager.gameData.map.isEventSet[1] = true;
        saveManager.gameData.map.isRewardSet[1] = true;
    }

    public void SetEnemyPort3()  // 외부 참조용
    {
        battleNode.SetEnemyPortOption(3);
        saveManager.gameData.map.isEventSet[2] = true;
        saveManager.gameData.map.isRewardSet[2] = true;
    }

    public void Play_BattleNode()
    {
        for (int i = 0; i < 3; i++)
        {
            saveManager.gameData.map.isEventSet[i] = false;
            saveManager.gameData.map.isRewardSet[i] = false;
        }
        battleNode.Play_BattleNode();
    }

    public void Play_BossNode()
    {
        for (int i = 0; i < 3; i++)
            saveManager.gameData.map.isEventSet[i] = false;
        battleNode.Play_BattleNode();
    }


    public void See_InfoPanel()
    {
        List<Option> option = new List<Option>();
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
