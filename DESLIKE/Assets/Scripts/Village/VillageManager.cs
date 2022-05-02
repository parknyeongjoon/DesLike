using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    public VillageNode villageNode;
    SaveManager saveManager;
    
    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curWindow = CurWindow.Village;
        dataUpdate();
    }

    void dataUpdate()
    {
        villageNode = (VillageNode)saveManager.map.curMapNode;   
    }

    public void EndVillage()
    {
        villageNode.End_VillageNode();
    }
}