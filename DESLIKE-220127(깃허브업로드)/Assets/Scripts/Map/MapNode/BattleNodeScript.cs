using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetEnemyPort()  // 외부 참조용
    {
        battleNode.SetenemyPortOption();
    }

    public void Play_BattleNode()
    {
        battleNode.Play_BattleNode();
    }

    public void See_InfoPanel()
    {
        Debug.Log("See Info");
        InfoPanel.SetActive(true);
        List<Option> option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(InfoTemp);
        for (int i = 0; i < option.Count; i++) // 주요 병력 3개만 보여주기
        {
            // 병력 전투력순 정렬
            // 주요 병력 3개 고르기
            if (i < 3)
            {
                createPrefab = Instantiate(InfoPrefab, InfoTemp.transform);
                createPrefab.GetComponent<RectTransform>().position = new Vector3(330, 440 - 70 * i, 0); // 3줄
                createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
                createPrefab.GetComponentInChildren<Text>().text = "총 유닛 수 : " 
                    + option[i].portNum.Length.ToString() + "마리";
            }
        }
    }
    
    public void See_More()
    {
        Debug.Log("See More");
        MoreInfoPanel.SetActive(true);
        List<Option> option = battleNode.enemyPortOption.soldierOption;
        GameObject createPrefab;
        GameManager.DeleteChilds(MoreTemp);
        for (int i = 0; i < option.Count; i++)  // 병력 전체 정렬
        {
            createPrefab = Instantiate(MorePrefab, MoreTemp.transform);
            createPrefab.GetComponent<RectTransform>().position = new Vector3((i % 5) * 27 + 369, 435 - 27 * (i / 5), 0); // 5개가 넘어가면 다음 줄로
            createPrefab.GetComponentInChildren<Image>().sprite = option[i].soldierData.sprite;
            createPrefab.GetComponentInChildren<Text>().text = option[i].portNum.Length.ToString();
        }
    }
}
