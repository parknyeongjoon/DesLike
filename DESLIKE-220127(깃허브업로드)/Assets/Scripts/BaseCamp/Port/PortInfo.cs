using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortInfo : MonoBehaviour
{
    [SerializeField] PortData portData;
    [SerializeField] GameObject SellBtn, UnlockBtn;
    [SerializeField] Text unlockPrice;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        if (portData.unlock) { image.color = new Color(1, 1, 1); }//포트가 해제되어있다면 흰색
        else { image.color = new Color(0.3f, 0.3f, 0.3f); }//아니라면 회색
        portData.portImg = image;
    }

    void OnEnable()
    {
        if (portData.soldierCode == "") { portData.soldierCode = null; }//지우기
        if (portData.soldierCode != null)
        {
            image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
    }

    public void SetPortCode()
    {
        if(PortManager.Instance.isSet && portData.unlock && portData.soldierCode == null)//set 상태면 빈 포트에 병사 적용
        {
            PortManager.Instance.isSet = false;
            portData.soldierCode = PortManager.Instance.soldierCode;
            image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
        else if(!PortManager.Instance.isSet && portData.soldierCode != null)//병사가 들어있다면 판매
        {
            PortManager.Instance.ControllActiveBtn(SellBtn);
        }
        else if(!PortManager.Instance.isSet && !portData.unlock)//포트가 잠겨있다면 포트를 언락하는 버튼 열기
        {
            //unlockPrice 값 변경해주기
            PortManager.Instance.ControllActiveBtn(UnlockBtn);
        }
    }

    public void SellPort()
    {
        //SoldierData tempSoldier = SaveManager.Instance.activeSoldierList[portData.soldierCode];
        //SaveManager.Instance.gameData.goodsCollection.food += (int)(tempSoldier.cost * 0.7f);//골드로 바꾸기
        //battleUIManager.infoPanel.SetMoneyText();//바꿔줘야함
        //tempSoldier.remain += 1;
        //돈 빼기
        //병사 수 -1
        //병사 수가 0이라면 밑에 버튼 삭제
        portData.soldierCode = null;
        portData.portImg.sprite = null;
        SellBtn.SetActive(false);
    }

    public void UnlockPort()
    {
        //돈 빼기
        UnlockBtn.SetActive(false);
        portData.unlock = true;
        image.color = new Color(1, 1, 1);
    }
}