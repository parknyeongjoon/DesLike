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

    public void TrainingOption1() // 1老 家葛
    {
    }

    public void TrainingOption2()   // 2老 家葛
    {
    }

    public void TrainingOption3()   // 3老 家葛
    {
    }

}
