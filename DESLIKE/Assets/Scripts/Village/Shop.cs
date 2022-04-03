using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;

    public void OpenBtn()
    {
        shopPanel.SetActive(true);
    }

    public void CloseBtn()
    {
        shopPanel.SetActive(false);
    }
}
