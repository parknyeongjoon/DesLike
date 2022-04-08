using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrganManager : MonoBehaviour
{
    void OnEnable()
    {
        SaveManager.Instance.gameData.mapData.curWindow = CurWindow.Organ;    
    }

    public void Back_Button()
    {
        SceneManager.LoadScene("Map");
    }
}