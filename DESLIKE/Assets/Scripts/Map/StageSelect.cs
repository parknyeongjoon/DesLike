using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StageSelect : MonoBehaviour
{
    int curStage;
    [SerializeField] Button FirstBtn, SecondBtn, ThirdBtn;

    void Awake()
    {
        SaveManager saveManager = SaveManager.Instance;
        curStage = saveManager.gameData.mapData.curStage;

        FirstBtn.gameObject.SetActive(false);
        SecondBtn.gameObject.SetActive(false);
        ThirdBtn.gameObject.SetActive(false);

        switch (curStage)
        {
            case 0:
                FirstBtn.gameObject.SetActive(true);
                break;
            case 1:
                SecondBtn.gameObject.SetActive(true);
                break;
            case 2:
                ThirdBtn.gameObject.SetActive(true);
                break;
        }
    }

    public void Stage1()
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curDay = 0;
        SceneManager.LoadScene("Map");
    }

    public void Stage2()
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curDay = 0;
        SceneManager.LoadScene("Map");
    }

    public void Stage3()
    {
        SaveManager saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curDay = 0;
        SceneManager.LoadScene("Map");
    }

}
