using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilTraining : MonoBehaviour
{
    public GameObject TrainingPanel;

    public void OpenTrainingPanel()
    {
        TrainingPanel.SetActive(true);
    }

    public void CloseTrainingPanel()
    {
        TrainingPanel.SetActive(false);
    }

}
