using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoldierRewardBtn : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject mutantPanel, changeMutantPanel;
    [SerializeField]
    MutantPanelScript mutantPanelScript;
    [SerializeField]
    ChangeMutantPanelScript changeMutantPanelScript;

    

    public SoldierData soldierReward;
    public int soldierRemain;
    SoldierData tempSoldier;

    public void GetReward()
    {
       if(soldierReward != null)
        {
            if (SaveManager.Instance.activeSoldierList.ContainsKey(soldierReward.code))
            {
                tempSoldier = SaveManager.Instance.activeSoldierList[soldierReward.code];
                mutantPanel.SetActive(true);
            }
            else
            {
                tempSoldier = Instantiate(soldierReward);
                tempSoldier.remain += soldierRemain;
                SaveManager.Instance.activeSoldierList.Add(tempSoldier.code, tempSoldier);
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
