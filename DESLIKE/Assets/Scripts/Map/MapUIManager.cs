using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapUIManager : MonoBehaviour
{
    [SerializeField]
    Button resume_Btn, mainTitle_Btn, quit_Btn;
    [SerializeField]
    GameObject menuPanel;

    SaveManager saveManager;

    void Awake()
    {
        saveManager = SaveManager.Instance;
        AkSoundEngine.PostEvent("Music_Map", gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(true);
        }
    }

    public void resume()
    {
        menuPanel.SetActive(false);
    }

    public void mainTitle()
    {
        saveManager.SaveGameData();
        Destroy(GameObject.Find("Hero"));
        SceneManager.LoadScene("MainTitle");
    }

    public void Quit()
    {
        SaveManager.Instance.SaveNExit();
    }
}
