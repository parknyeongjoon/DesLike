using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BattleNode",menuName = "ScriptableObject/MapNodeT/BattleNode")]
public class BattleNode : MapNode
{
    public List<PortsOption> enemyPortsOptions;
    public PortsOption enemyPortOption;
    public List<PortsOption> challengeList;
    public PortsOption challengeOption;
    public PortDatas enemyPortDatas;

    public bool isChallenge;

    public void SetenemyPortOption()
    {
        int temp = Random.Range(0, enemyPortsOptions.Count);
        enemyPortOption = enemyPortsOptions[temp];
    }

    public void SetChallengeOption()
    {
        int temp = Random.Range(0, challengeList.Count);
        challengeOption = challengeList[temp];
    }

    public void Play_BattleNode()
    {
        if (isPlayable)
        {
            List<Option> option = enemyPortOption.SoldierOption;
            for (int i = 0; i < 50; i++)
            {
                enemyPortDatas.portDatas[i].soldierCode = null;
            }
            enemyPortDatas.activeSoldierList = new Dictionary<string, SoldierData>();
            for (int i = 0; i < option.Count; i++)
            {
                SoldierData tempSoldier = Instantiate(option[i].soldierData);
                if (option[i].mutant)
                {
                    tempSoldier.mutant = option[i].mutant;
                }
                enemyPortDatas.activeSoldierList.Add(tempSoldier.code, tempSoldier);
                for (int j = 0; j < option[i].portNum.Length; j++)
                {
                    enemyPortDatas.portDatas[option[i].portNum[j]].soldierCode = tempSoldier.code;
                }
            }
            map.curMapNode = this;
            SceneManager.LoadScene("Battle Field");
        }
    }
}
