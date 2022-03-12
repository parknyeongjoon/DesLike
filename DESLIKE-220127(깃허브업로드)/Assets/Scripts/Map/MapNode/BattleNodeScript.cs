using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleNodeScript : NodeScript
{
    BattleNode battleNode;
    [SerializeField] GameObject InfoPanel, InfoTemp, InfoPrefab, MoreInfoPanel, MoreTemp, MorePrefab;

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

    public void See_InfoPanel()
    {
        InfoPanel.SetActive(true);
        List<Option> option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(InfoTemp);
        for (int i = 0; i < option.Count; i++) // �ֿ� ���� 3���� �����ֱ�
        {
            // ���� �����¼� ����
            // �ֿ� ���� 3�� ����
            if (i < 3)
            {
                createPrefab = Instantiate(InfoPrefab, InfoTemp.transform);
                createPrefab.GetComponent<RectTransform>().position = new Vector3(330, 440 - 70 * i, 0); // 3��
                createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
                createPrefab.GetComponentInChildren<TMP_Text>().text = "�� ���� ��Ʈ : " 
                    + option[i].portNum.Length.ToString() + "��Ʈ";
            }
        }
    }
    
    public void See_More()
    {
        MoreInfoPanel.SetActive(true);
        List<Option> option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(MoreTemp);
        for (int i = 0; i < option.Count; i++)  // ���� ��ü ����
        {
            createPrefab = Instantiate(MorePrefab, MoreTemp.transform);
            createPrefab.GetComponent<RectTransform>().position = new Vector3((i % 5) * 90 + 369, 435 - 27 * (i / 5), 0); // 5���� �Ѿ�� ���� �ٷ�
            createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
            createPrefab.GetComponentInChildren<TMP_Text>().text = option[i].portNum.Length.ToString();
        }
    }
}
