using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Soldier_portrait : MonoBehaviour
{
    BattleUIManager battleUIManager;

    [SerializeField]
    Image portrait_Image;
    [SerializeField]
    Text remain, money, jewel;
    GoodsCollection goodsCollection;

    public SoldierData soldier;

    void Awake()
    {
        battleUIManager = BattleUIManager.Instance;
        goodsCollection = SaveManager.Instance.gameData.goodsCollection;
    }

    void OnEnable()
    {
        if(soldier != null)
        {
            remain.text = soldier.remain.ToString();
            money.text = soldier.cost.ToString();
        }
    }

    void Start()
    {
        portrait_Image.sprite = soldier.sprite;
        remain.text = soldier.remain.ToString();
        money.text = soldier.cost.ToString();
    }

    public void CreateSoldier()
    {
        //할당된 유닛코드가 없으면 유닛코드 설정
        if (battleUIManager.cur_Port.soldierCode == null && goodsCollection.food >= soldier.cost && soldier.remain > 0)
        {
            goodsCollection.food -= soldier.cost;
            battleUIManager.infoPanel.SetMoneyText();
            soldier.remain -= 1;
            battleUIManager.cur_Port.soldierCode = soldier.code;
            battleUIManager.cur_Port.portImg.sprite = soldier.sprite;
            battleUIManager.SetMidPanel(2);
        }//돈 부족할 때 추가
    }
}
