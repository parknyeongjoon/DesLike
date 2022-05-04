using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilPortCenter : MonoBehaviour
{
    public GameObject PortCenterPanel;

    public void OpenPortCenter()
    {
        PortCenterPanel.SetActive(true);
    }

    public void ClosePortCenter()
    {
        PortCenterPanel.SetActive(false);
    }

}
