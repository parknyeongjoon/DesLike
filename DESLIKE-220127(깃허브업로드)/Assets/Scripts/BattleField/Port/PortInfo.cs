using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortInfo : MonoBehaviour
{
    BattleUIManager battleUIManager;

    [SerializeField]
    PortData portData;
    [SerializeField]
    Image image;

    void Awake()
    {
        battleUIManager = BattleUIManager.Instance;
        portData.portImg = image;
    }

    void OnEnable()
    {
        if (portData.soldierCode != null)
        {
            portData.portImg.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
    }

    public void SelectPort()
    {
        battleUIManager.cur_Port = portData;
    }
}