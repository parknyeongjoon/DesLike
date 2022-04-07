using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicUI : MonoBehaviour
{
    [SerializeField] TMP_Text HpText, CurDayText, GoldText;
    SaveManager saveManager;
    int curDay, curGold;
    float curHp;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        UpdateData();
        UpdateText();
    }

    void UpdateData()
    {
        curDay = saveManager.gameData.mapData.curDay;
        curHp = saveManager.gameData.heroSaveData.cur_Hp;
        curGold = saveManager.gameData.goodsSaveData.gold;
    }

    void UpdateText()
    {
        GoldText.text = "- °ñµå : " + curGold;
        CurDayText.text = curDay + " / 30";
        HpText.text = curHp + " / 500";
    }
}
