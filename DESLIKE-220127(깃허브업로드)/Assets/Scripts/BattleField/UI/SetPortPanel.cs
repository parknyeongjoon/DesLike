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
        //포트 정보 추가
    }
}
