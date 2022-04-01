using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortInfo : MonoBehaviour
{
    [SerializeField] PortData portData;
    [SerializeField] GameObject SellBtn, UnlockBtn;
    [SerializeField] TMP_Text unlockPrice;
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
        if(PortManager.Instance.portState == Port_State.Set)//set 상태일 때
        {
            if(portData.unlock && portData.soldierCode == null)//언락된 빈 포트면 병사 적용
            {
                PortManager.Instance.portState = Port_State.Idle;
                portData.soldierCode = PortManager.Instance.rewardSoldierCode;
                image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
            }
        }
        else if(PortManager.Instance.portState == Port_State.Idle)//idle 상태일 때
        {
            if(!portData.unlock)//포트가 잠겨있다면 포트를 언락하기
            {
                PortManager.Instance.ControllActiveBtn(UnlockBtn);
                //unlockPrice.text = 설정.ToString();
            }
        }
        else if(PortManager.Instance.portState == Port_State.Sell)
        {
            if (portData.soldierCode != null)//병사가 들어있다면 판매
            {
                PortManager.Instance.ControllActiveBtn(SellBtn);
            }
        }
    }

    public void SellPort()
    {
        //SaveManager.Instance.gameData.goodsCollection.food += (int)(tempSoldier.cost * 0.7f);//골드로 바꾸기
        //돈 추가
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

    public void PortDragStart()
    {
        if(PortManager.Instance.portState == Port_State.Idle && portData.soldierCode != null)
        {
            PortManager.Instance.portState = Port_State.Drag;
            PortManager.Instance.originPort = portData;
            if (PortManager.Instance.activeBtn != null) { PortManager.Instance.activeBtn.SetActive(false); }
            PortManager.Instance.DragPortImg();
        }
    }

    public void PortDragEnd()
    {
        StartCoroutine(DragEndCoroutine());
    }

    IEnumerator DragEndCoroutine()
    {
        yield return null;
        if(PortManager.Instance.portState == Port_State.Drag)
        {
            PortManager.Instance.portState = Port_State.Idle;
            PortManager.Instance.ReturnPortImg();
        }
    }

    public void PortDrop()
    {
        if(PortManager.Instance.portState == Port_State.Drag && portData.unlock)
        {
            if(portData.soldierCode == null)
            {
                portData.soldierCode = PortManager.Instance.originPort.soldierCode;
                PortManager.Instance.originPort.soldierCode = null;
                PortManager.Instance.originPort.portImg.sprite = null;
            }
            else if(portData.soldierCode != null)
            {
                string tempSoldierCode;
                tempSoldierCode = portData.soldierCode;
                portData.soldierCode = PortManager.Instance.originPort.soldierCode;
                PortManager.Instance.originPort.soldierCode = tempSoldierCode;
                PortManager.Instance.originPort.portImg.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[PortManager.Instance.originPort.soldierCode].sprite;
            }
            image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
    }

    public void PortPointEnter()
    {
        if(PortManager.Instance.portState == Port_State.Drag)//드래그 중에 마우스 커서가 들어가면 색깔이 바뀌는 함수
        {
            if (PortManager.Instance.originPort != portData && portData.unlock)//드래그를 시작한 포트거나 잠겨있는 포트면 제외
            {
                image.color = new Color(0.7f, 0, 0.7f);
            }
        }
        else if(PortManager.Instance.portState == Port_State.Set)//세팅중이라면
        {
            if (portData.unlock && portData.soldierCode == null)
            {
                image.color = new Color(0.7f, 0, 0.7f);
            }
        }
    }

    public void PortPointExit()
    {
        if(PortManager.Instance.portState == Port_State.Drag)
        {
            if (PortManager.Instance.originPort != portData && portData.unlock)//드래그를 시작한 포트거나 잠겨있는 포트면 제외
            {
                image.color = new Color(0, 0.7f, 0);
            }
        }
        else if (PortManager.Instance.portState == Port_State.Set)//세팅중이라면
        {
            if (portData.unlock && portData.soldierCode == null)
            {
                image.color = new Color(0, 0.7f, 0);
            }
        }
    }
}