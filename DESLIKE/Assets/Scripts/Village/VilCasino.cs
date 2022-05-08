using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilCasino : MonoBehaviour
{
    public GameObject CasinoPanel;

    public void OpenCasinioPanel()
    {
        CasinoPanel.SetActive(true);
    }

    public void CloseCasinoPanel()
    {
        CasinoPanel.SetActive(false);
    }
}
