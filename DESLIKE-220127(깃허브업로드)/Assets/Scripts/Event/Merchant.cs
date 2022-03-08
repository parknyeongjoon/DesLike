using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    SaveManager saveManager;

    void OnEnable()
    {
        SaveManager saveManager = SaveManager.Instance;
        Debug.Log("Merchant");
    }

    public void EndButton()
    {
        saveManager.gameData.map.curDay += 2;
    }
}
