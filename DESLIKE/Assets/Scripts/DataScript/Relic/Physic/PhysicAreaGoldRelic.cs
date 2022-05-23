using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PhysicAreaGoldRelic",menuName ="ScriptableObject/RelicT/Physic/PhysicAreaGoldRelic")]
public class PhysicAreaGoldRelic : InstanceRelicData
{
    [SerializeField] Tribe effectTribe;
    [SerializeField] int effectAmount;

    public override void Effect()
    {
        foreach(var soldierData in SaveManager.Instance.allyPortDatas.activeSoldierList.Values)
        {
            if (soldierData.tribe == effectTribe)
            {
                SaveManager.Instance.gameData.goodsSaveData.areaGold += effectAmount;
            }
        }
    }
}
