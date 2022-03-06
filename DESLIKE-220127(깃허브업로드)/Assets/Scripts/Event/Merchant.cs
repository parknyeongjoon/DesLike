using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("Merchant");
    }

    public void EndButton()
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.map.curDay += 2;
    }
}
