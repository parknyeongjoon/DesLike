using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VilHealing : MonoBehaviour
{
    public GameObject HealPanel, CheckPanel, ErrorPanel;
    public TMP_Text InformText, CheckText, Cur_HPText;
    SaveManager saveManager;
    int healCount;
    int temp_Max_HP = 500;
    float cur_HP;
    int curGold;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        HealPanel.SetActive(false);
        DataUpdate();
        PriceUpdate();
    }

    void DataUpdate()
    {
        healCount = saveManager.gameData.villageData.healCount;
        cur_HP = saveManager.gameData.heroSaveData.cur_Hp;
        curGold = saveManager.gameData.goodsSaveData.gold;
    }

    void PriceUpdate()
    {
        if (healCount == 0) InformText.text = "휴식하기\n(1회 무료)";
        else InformText.text = "휴식하기\n(" + healCount * 50 + "골드 소모)";
        Cur_HPText.text = "현재 체력 : " + cur_HP + " / " + temp_Max_HP;
    }

    public void HealPanelOpen()
    {
        HealPanel.SetActive(true);
        CheckPanel.SetActive(false);
    }

    public void HealPanelClose()
    {
        HealPanel.SetActive(false);
    }

    public void CheckPanelOpen()
    {
        CheckPanel.SetActive(true);
        if(healCount == 0) CheckText.text = "회복하시겠습니까?\n(1회 무료)";
        else CheckText.text = "회복하시겠습니까?\n(소모골드 : " + healCount * 50 + "골드)";
    }

    public void CheckPanelClose()
    {
        CheckPanel.SetActive(false);
    }
    
    public void Try_Heal()
    {
        if (curGold - healCount * 50 < 0)
            ErrorPanel.SetActive(true);
        else
        {
            curGold -= healCount * 50;
            Heal_HP();
            healCount++;
        }
    }

    void Heal_HP()
    {
        cur_HP += temp_Max_HP / 10;
        if (cur_HP >= temp_Max_HP)
            cur_HP = temp_Max_HP;
    }

    public void ErrorPanelClose()
    {
        ErrorPanel.SetActive(false);
        CheckPanelClose();
    }
}
