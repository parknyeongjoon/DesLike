using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoldierRewardBtn : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject PortCanvas;



    public string soldierCode;
    SoldierData tempSoldier;

    public void GetReward()
    {
       if(soldierCode != null)
        {
            if (SaveManager.Instance.allyPortDatas.activeSoldierList.ContainsKey(soldierCode))//이미 병사를 가지고 있다면 포트 한 칸 더 생성하기와 특화 중에 하나 선택하기
            {
                Instantiate(PortCanvas);
                PortManager.Instance.rewardSoldierCode = soldierCode;
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
}
