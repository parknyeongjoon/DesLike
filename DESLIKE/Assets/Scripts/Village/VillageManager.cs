using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{
    void OnEnable()
    {
        SaveManager.Instance.gameData.mapData.curWindow = CurWindow.Village;
    }
}