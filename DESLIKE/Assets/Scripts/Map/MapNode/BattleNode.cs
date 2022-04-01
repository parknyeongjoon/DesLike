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
    public PortDatas enemyPortDatas;
    public bool isChallenge;
    SaveManager saveManager;

    void Enable()
    {
        saveManager = SaveManager.Instance;
    }

    public void SetEnemyPortOption(int i)
    {
        bool isEventSet = saveManager.gameData.map.isEventSet[i];
        if (isEventSet == false)    // 새로운 정보 세팅
        {
            int temp = Random.Range(0, enemyPortsOptions.Count);
            enemyPortOption = enemyPortsOptions[temp];
        }
        else   // 기존 정보 가져오기
        {
            int nextEvent = saveManager.gameData.map.nextEvent[i];
            enemyPortOption = enemyPortsOptions[nextEvent];
        }
    }

    public void SetChallengeOption()
    {
        int temp = Random.Range(0, challengeList.Count);
    }

    public void Play_BattleNode()
    {
        List<Option> option = new List<Option>();
        option = enemyPortOption.soldierOption;
        for (int i = 0; i < enemyPortDatas.portDatas.Length; i++)
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
