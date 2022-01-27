using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNodeScript : NodeScript
{
    BattleNode battleNode;
    [SerializeField]
    GameObject BattleNodePanel, previewPrefab;

    void Start()
    {
        battleNode = (BattleNode)MapNode;
        battleNode.SetenemyPortOption();
        battleNode.SetChallengeOption();
    }

    public void Play_BattleNode()
    {
        battleNode.Play_BattleNode();
    }

    public void See_BattleNode()
    {
        if (battleNode.isPlayable)
        {
            GameObject createPrefab;
            List<Option> option = battleNode.enemyPortOption.SoldierOption;
            GameManager.DeleteChilds(BattleNodePanel);
            BattleNodePanel.SetActive(true);
            for (int i = 0; i < option.Count; i++)
            {
                createPrefab = Instantiate(previewPrefab, BattleNodePanel.transform);
                createPrefab.GetComponent<Image>().sprite = option[i].soldierData.sprite;
                createPrefab.GetComponentInChildren<Text>().text = option[i].portNum.Length.ToString();
            }
        }
    }

    public void Close_See_BattleNode()
    {
        BattleNodePanel.SetActive(false);
    }
}
