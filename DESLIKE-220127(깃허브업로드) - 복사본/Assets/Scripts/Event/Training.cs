using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    SaveManager saveManager;
    GoodsCollection goodsCollection;
    int level;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        level = saveManager.gameData.map.level;
        goodsCollection = saveManager.gameData.goodsCollection;

        Debug.Log("Traing");
    }

    public void TrainingOption1() // 1�� �Ҹ�
    {
    }

    public void TrainingOption2()   // 2�� �Ҹ�
    {
    }

    public void TrainingOption3()   // 3�� �Ҹ�
    {
    }

}
