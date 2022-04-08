using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortInfo : MonoBehaviour
{
    [SerializeField] PortData portData;
    [SerializeField] GameObject SellBtn, UnlockBtn, highLightImg;
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
        if (portData.soldierCode != "")
        {
            image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
        }
    }

    public void SetPortCode()
    {
        if(PortManager.Instance.portState == Port_State.SetSoldier)//set 상태일 때
        {
            if(portData.unlock && portData.soldierCode == "")//언락된 빈 포트면 병사 적용
            {
                PortManager.Instance.portState = Port_State.Idle;
                portData.soldierCode = PortManager.Instance.soldierReward.soldier.code;
                if (PortManager.Instance.soldierReward.mutant != null) { portData.mutantCode = PortManager.Instance.soldierReward.mutant.code; }
                image.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[portData.soldierCode].sprite;
            }
        }
        else if(PortManager.Instance.portState == Port_State.SetMutant)//뮤턴트 적용시에
        {
            if(portData.soldierCode == PortManager.Instance.soldierReward.soldier.code)
            {
                PortManager.Instance.originPort = portData;
                PortManager.Instance.ControllActiveBtn(highLightImg);
                PortManager.Instance.mutantOKBtn.SetActive(true);
                //껐다켜서 OnEnable 실행시키는건데 다른 방법도 있을 듯
                PortManager.Instance.rewardMutantPanel.SetActive(false);
                PortManager.Instance.rewardMutantPanel.SetActive(true);
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
            if (portData.soldierCode != "")//병사가 들어있다면 판매
            {
                PortManager.Instance.ControllActiveBtn(SellBtn);
            }
        }
    }

    public void SellPort()
    {
        //SaveManager.Instance.gameData.goodsCollection.food += (int)(tempSoldier.cost * 0.7f);//골드로 바꾸기
        //돈 추가
        portData.soldierCode = "";
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
        if(PortManager.Instance.portState == Port_State.Idle && portData.soldierCode != "")
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
            if(portData.soldierCode == "")
            {
                portData.soldierCode = PortManager.Instance.originPort.soldierCode;
                PortManager.Instance.originPort.soldierCode = "";
                PortManager.Instance.originPort.portImg.sprite = null;
            }
            else if(portData.soldierCode != "")
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
        if (PortManager.Instance.portState == Port_State.Drag)//드래그 중에 마우스 커서가 들어가면 하이라이트 이미지가 생기는 함수
        {
            if (PortManager.Instance.originPort != portData && portData.unlock)//드래그를 시작한 포트거나 잠겨있는 포트면 제외
            {
                PortManager.Instance.ControllActiveBtn(highLightImg);
            }
        }
        else if (PortManager.Instance.portState == Port_State.SetSoldier)//세팅중이라면
        {
            if (portData.unlock && portData.soldierCode == "")
            {
                PortManager.Instance.ControllActiveBtn(highLightImg);
            }
        }
        else if (PortManager.Instance.portState == Port_State.SetMutant)//뮤턴트 세팅중이라면
        {
            if(portData.soldierCode == PortManager.Instance.soldierReward.soldier.code && PortManager.Instance.originPort == null)
            {
                PortManager.Instance.ControllActiveBtn(highLightImg);
            }
        }
    }

    public void PortPointExit()
    {
        if(PortManager.Instance.portState == Port_State.Drag)
        {
            if (PortManager.Instance.originPort != portData && portData.unlock)//드래그를 시작한 포트거나 잠겨있는 포트면 제외
            {
                highLightImg.SetActive(false);
            }
        }
        else if (PortManager.Instance.portState == Port_State.SetSoldier)//세팅중이라면
        {
            if (portData.unlock && portData.soldierCode == "")
            {
                highLightImg.SetActive(false);
            }
        }
        else if (PortManager.Instance.portState == Port_State.SetMutant)//뮤턴트 세팅중이라면
        {
            if (portData.soldierCode == PortManager.Instance.soldierReward.soldier.code && PortManager.Instance.originPort == null)
            {
                highLightImg.SetActive(false);
            }
        }
    }
}