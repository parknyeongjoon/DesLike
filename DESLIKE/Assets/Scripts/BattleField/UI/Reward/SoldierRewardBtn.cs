using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoldierRewardBtn : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject mutantPanel, changeMutantPanel, PortCanvas;
    [SerializeField]
    MutantPanelScript mutantPanelScript;
    [SerializeField]
    ChangeMutantPanelScript changeMutantPanelScript;



    public string soldierCode;
    SoldierData tempSoldier;

    public void GetReward()
    {
       if(soldierCode != null)
        {
            if (SaveManager.Instance.activeSoldierList.ContainsKey(soldierCode))
            {
                tempSoldier = SaveManager.Instance.activeSoldierList[soldierCode];
                mutantPanel.SetActive(true);
            }
            else
            {
                Instantiate(PortCanvas);
                PortManager.Instance.rewardSoldierCode = soldierCode;
                StartCoroutine(PortManager.Instance.SetSoldierCoroutine());
                //SoldierManager.GetSoldier(soldierCode, null);
            }
            button.interactable = false;
        }
    }

    public void ChangeMutant()
    {
        if (tempSoldier.mutant)
        {
            changeMutantPanelScript.soldierData = tempSoldier;
            changeMutantPanelScript.changeMutant = mutantPanelScript.mutant;
            changeMutantPanel.SetActive(true);
        }
        else
        {
            OkMutant();
        }
    }

    public void OkMutant()
    {
        tempSoldier.mutant = mutantPanelScript.mutant;
        mutantPanel.SetActive(false);
        changeMutantPanel.SetActive(false);
        Debug.Log("설정됨");
        //mutant 설정 완료 화면 띄워주기
    }
}
