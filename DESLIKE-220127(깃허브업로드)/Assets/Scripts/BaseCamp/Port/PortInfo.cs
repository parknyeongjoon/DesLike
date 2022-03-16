using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortInfo : MonoBehaviour
{
    BattleUIManager battleUIManager;

    [SerializeField]
    PortData portData;
    Image image;

    void Awake()
    {
        battleUIManager = BattleUIManager.Instance;
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        if (portData.soldierCode != null)
        {
            image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
    }

    public void SelectPort()
    {
        battleUIManager.cur_Port = portData;
    }
}