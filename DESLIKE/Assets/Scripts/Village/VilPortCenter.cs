using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilPortCenter : MonoBehaviour
{
    int curGold;
    [SerializeField] GameObject PortCenterPanel, ErrorPanel, SelectPanel;

    private void OnEnable()
    {
        curGold = SaveManager.Instance.gameData.goodsSaveData.gold;
    }

    public void OpenPortCenter()
    {
        PortCenterPanel.SetActive(true);
    }

    public void ClosePortCenter()
    {
        PortCenterPanel.SetActive(false);
    }

    public void BuyMutant()
    {
        if (curGold < 25) ErrorPanel.SetActive(true);
        else SelectPanel.SetActive(true);
    }

    public void RerollMutant()
    {
        if (curGold < 15) ErrorPanel.SetActive(true);
        else SelectPanel.SetActive(true);
    }

    public void CloseSelectPanel()
    {
        SelectPanel.SetActive(false);
    }

    public void CloseErrorPanel()
    {
        ErrorPanel.SetActive(false);
    }
}
