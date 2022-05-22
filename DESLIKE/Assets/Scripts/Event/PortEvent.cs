using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortEvent : EventBasic
{
    int curGold;
    bool isMutantBuy, isMutuntReroll;
    

    [SerializeField] Button[] buttons = new Button[2];
    [SerializeField] TMP_Text[] buttonsTMP = new TMP_Text[2];
    [SerializeField] GameObject ErrorPanel, BackPanel;
    [SerializeField] PortDatas portDatas;


    void OnEnable()
    {
        LoadData();
        SaveData();
    }

    void LoadData()
    {
        LoadComData();
        LoadPortEData();
    }

    void LoadPortEData()
    {
        curGold = saveManager.gameData.goodsSaveData.gold;
    }

    void SaveData()
    {
        SaveComData();
        SavePortEData();
        saveManager.SaveGameData();
    }

    void SavePortEData()
    {
        saveManager.gameData.goodsSaveData.gold = curGold;
    }

    public void BuyMutant()
    {
        if (curGold < 25)
            ErrorPanel.SetActive(true);
        else BackPanel.SetActive(true);

    }

    public void RerollMutant()
    {
        if (curGold < 15)
            ErrorPanel.SetActive(true);
        else BackPanel.SetActive(true);

    }

    public void CancelMutant()
    {
        BackPanel.SetActive(false);
    }

    public void ErrorPanelClose()
    {
        ErrorPanel.SetActive(false);
    }
}