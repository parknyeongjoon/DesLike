using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject ArenaPanel;

    public void OpenBtn()
    {
        ArenaPanel.SetActive(true);
    }

    public void CloseBtn()
    {
        ArenaPanel.SetActive(false);
    }
}
