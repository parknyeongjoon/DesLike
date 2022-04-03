using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrganManager : MonoBehaviour
{
    public void Back_Button()
    {
        SceneManager.LoadScene("Map");
    }
}