using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    SaveManager saveManager;
    int level;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        level = saveManager.map.level;

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
