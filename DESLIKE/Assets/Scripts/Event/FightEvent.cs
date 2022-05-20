using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightEvent : EventBasic
{
    int enPortLevel, curStage;
    int phyNorRelC, speNorRelC, comNorRelC, phyEpicRelC, speEpicRelC, comEpicRelC;
    bool isAlreadySelect;

    [SerializeField] Button[] Buttons = new Button[2];
    [SerializeField] TMP_Text[] OptionText = new TMP_Text[2];
    [SerializeField] BattleNode battleNode;

    void OnEnable()
    {
        LoadData();

        if (isAlreadySelect == true)
        {
            for (int i = 0; i < 2; i++)
                Buttons[i].gameObject.SetActive(false);
            EndButton.gameObject.SetActive(true);
        }
        else
            EBattleSet();

        SaveData();
    }

    void SaveData()
    {
        SaveFightEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SaveFightEData()
    {
        saveManager.gameData.mapData.curStage = curStage;
        saveManager.gameData.eventData.isAlreadySelect = isAlreadySelect;
    }

    void LoadData()
    {
        LoadFightEData();
        LoadComData();
    }

    void LoadFightEData()
    {
        curStage = saveManager.gameData.mapData.curStage;
        isAlreadySelect = saveManager.gameData.eventData.isAlreadySelect;
    }

    void EBattleSet()
    {
        // Set_PortsOption(allyOption, allyPortDatas);
        // Set_PortsOption(enemyOption, enemyPortDatas);
        DataSet();
        LevelSet();
        RewardSet();
    }

    void DataSet()
    {
        phyNorRelC = map.physicNorRel.Count;
        speNorRelC = map.spellNorRel.Count;
        comNorRelC = map.commonNorRel.Count;
        phyEpicRelC = map.physicEpicRel.Count;
        speEpicRelC = map.spellEpicRel.Count;
        comEpicRelC = map.commonEpicRel.Count;
    }

    void LevelSet()
    {
        switch (curStage)    // 총 6단계
        {
            case 0:
                if (curDay <= 15) enPortLevel = 0;
                else enPortLevel = 1;
                break;
            case 1:
                if (curDay <= 15) enPortLevel = 2;
                else enPortLevel = 3;
                break;
            case 2:
                if (curDay <= 15) enPortLevel = 4;
                else enPortLevel = 5;
                break;
        }
        battleNode.enemyPortOption = battleNode.enemyPortsOptions[enPortLevel];
    }

    void RewardSet()    // 희귀 유물 보상만 
    {
        battleNode.SetAbleReward();
        battleNode.SetEpicRel();
    }

    void Set_PortsOption(PortsOption portsOption, PortDatas portDatas)
    {
        List<Option> option = portsOption.soldierOption;
        for (int i = 0; i < portDatas.portDatas.Length; i++)
        {
            portDatas.portDatas[i].soldierCode = "";
            portDatas.portDatas[i].mutantCode = "";
            portDatas.portDatas[i].unlock = false;
        }
        portDatas.activeSoldierList.Clear();
        for (int i = 0; i < option.Count; i++)
        {
            portDatas.activeSoldierList.Add(option[i].soldierData.code, Instantiate(option[i].soldierData));
            for (int j = 0; j < option[i].portNum.Length; j++)
            {
                portDatas.portDatas[option[i].portNum[j]].soldierCode = option[i].soldierData.code;
            }
        }
    }

    public void BattleBtn() // 3일 소모, 전투 시작
    {
        curDay += 3;
        isAlreadySelect = false;
        SaveData();

        battleNode.Play_BattleNode();
    }

    public void ThroughButton() // 1일 소모, 전투X
    {
        curDay += 1;
        for(int i = 0; i<2; i++)
            Buttons[i].gameObject.SetActive(false);
        EndButton.gameObject.SetActive(true);
        isAlreadySelect = true;
        SaveData();
    }
}
