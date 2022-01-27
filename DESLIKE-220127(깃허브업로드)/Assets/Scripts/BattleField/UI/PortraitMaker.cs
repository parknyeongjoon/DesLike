using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitMaker : MonoBehaviour
{
    [SerializeField]
    GameObject portrait_Prefab;

    GameObject createPortrait;
    UI_Soldier_portrait soldier_Portrait;

    void Awake()
    {
        foreach(SoldierData soldierData in SaveManager.Instance.activeSoldierList.Values)
        {
            createPortrait = Instantiate(portrait_Prefab, this.transform);
            soldier_Portrait = createPortrait.GetComponent<UI_Soldier_portrait>();
            soldier_Portrait.soldier = soldierData;
        }
    }
}