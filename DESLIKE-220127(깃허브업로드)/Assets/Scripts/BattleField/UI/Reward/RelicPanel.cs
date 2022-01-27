using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPanel : MonoBehaviour
{
    BattleUIManager battleUIManager;

    void Awake()
    {
        battleUIManager = BattleUIManager.Instance;
    }
}
