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
        battleNode = (BattleNode)MapNode;
        battleNode.SetenemyPortOption();
        battleNode.SetChallengeOption();
    }

    public void SetEnemyPort()  // �ܺ� ������
    {
        battleNode.SetenemyPortOption();
    }

    public void Play_BattleNode()
    {
        battleNode.Play_BattleNode();
    }

    public void Play_BossNode()
    {
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
