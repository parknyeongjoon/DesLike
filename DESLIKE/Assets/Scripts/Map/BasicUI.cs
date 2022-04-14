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

    static GameObject container;
    static BasicUI instance;

    public static BasicUI Instance
    {
        get
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "BasicUI";
                DontDestroyOnLoad(container);
                instance = container.AddComponent<BasicUI>();
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        saveManager = SaveManager.Instance;
        UpdateText();
    }

    public void UpdateText()
    {
        curDay = saveManager.gameData.mapData.curDay;
        curHp = saveManager.gameData.heroSaveData.cur_Hp;
        curGold = saveManager.gameData.goodsSaveData.gold;

        GoldText.text = "- °ñµå : " + curGold;
        CurDayText.text = curDay + " / 30";
        HpText.text = curHp + " / 500";
    }
}
