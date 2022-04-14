using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BattleNode", menuName = "ScriptableObject/MapNodeT/BattleNode")]
public class BattleNode : MapNode
{
    public List<PortsOption> enemyPortsOptions;
    public PortsOption enemyPortOption;
    public PortDatas enemyPortDatas;
    const int THREE = 3;
    public bool isChallenge;
    public bool[] isEventSet = new bool[THREE];
    public bool[] isRewardSet = new bool[THREE];
    public string curBattleString;
    
    public void Play_BattleNode()
    {
        enemyPortDatas.activeSoldierList.Clear();
        List<Option> option = new List<Option>();
        option = enemyPortOption.soldierOption;
        for (int i = 0; i < enemyPortDatas.portDatas.Length; i++)
        {
            enemyPortDatas.portDatas[i].soldierCode = "";
        }

        for (int i = 0; i < option.Count; i++)
        {
            SoldierData tempSoldier = Instantiate(option[i].soldierData);
            if (option[i].mutant)
            {
                // tempSoldier.mutantCode = option[i].mutantCode;
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