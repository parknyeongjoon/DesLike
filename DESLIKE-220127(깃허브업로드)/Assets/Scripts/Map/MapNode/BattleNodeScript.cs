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

    public void SetEnemyPort1()  // �ܺ� ������
    {
        battleNode.SetEnemyPortOption(1);
        saveManager.gameData.map.isEventSet[0] = true;
        saveManager.gameData.map.isRewardSet[0] = true;
    }

    public void SetEnemyPort2()  // �ܺ� ������
    {
        battleNode.SetEnemyPortOption(2);
        saveManager.gameData.map.isEventSet[1] = true;
        saveManager.gameData.map.isRewardSet[1] = true;
    }

    public void SetEnemyPort3()  // �ܺ� ������
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
