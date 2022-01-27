using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPortPanel : MonoBehaviour
{
    BattleUIManager battleUIManager;
    [SerializeField]
    Image soldier_Portrait;
    PortData portData;

    void Awake()
    {
        battleUIManager = BattleUIManager.Instance;
    }

    void OnEnable()
    {
        portData = battleUIManager.cur_Port;
        soldier_Portrait.sprite = portData.portImg.sprite;
    }

    public void SellPort()
    {
        SoldierData tempSoldier = SaveManager.Instance.activeSoldierList[portData.soldierCode];
        SaveManager.Instance.gameData.goodsCollection.food += (int)(tempSoldier.cost * 0.7f);
        battleUIManager.infoPanel.SetMoneyText();//event로 넣어버리기
        tempSoldier.remain += 1;
        portData.soldierCode = null;
        portData.portImg.sprite = null;
        battleUIManager.SetMidPanel(1);//이벤트로 넣을 수 있으려나?
    }
}
