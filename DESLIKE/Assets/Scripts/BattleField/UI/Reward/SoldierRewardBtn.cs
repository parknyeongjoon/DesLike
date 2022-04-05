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
            if (SaveManager.Instance.activeSoldierList.ContainsKey(soldierCode))//이미 병사를 가지고 있다면 포트 한 칸 더 생성하기와 특화 중에 하나 선택하기
            {
                tempSoldier = SaveManager.Instance.activeSoldierList[soldierCode];
                mutantPanel.SetActive(true);
            }
            else//안 갖고 있다면 바로 포트에 생성하기
            {
                Instantiate(PortCanvas);
                PortManager.Instance.rewardSoldierCode = soldierCode;
                StartCoroutine(PortManager.Instance.SetSoldierCoroutine());
                //SoldierManager.GetSoldier(soldierCode, null);
            }
            button.interactable = false;
        }
    }
    /*
    public void ChangeMutant()
    {
        if (tempSoldier.mutantCode != "0")  // 이것도 적당히 바꿔보십쇼-sh
        {
            changeMutantPanelScript.soldierData = tempSoldier;
            changeMutantPanelScript.changeMutant = mutantPanelScript.mutantCode;
            changeMutantPanel.SetActive(true);
        }
        else
        {
            OkMutant();
        }
    }

    public void OkMutant()
    {
        tempSoldier.mutantCode = mutantPanelScript.mutantCode;
        mutantPanel.SetActive(false);
        changeMutantPanel.SetActive(false);
        Debug.Log("설정됨");
        //mutant 설정 완료 화면 띄워주기
    }
    */
}
