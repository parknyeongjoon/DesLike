using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializing : MonoBehaviour
{
    void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        SaveManager saveManager = SaveManager.Instance;
        SceneManager.LoadScene("MainTitle");
    }
}
