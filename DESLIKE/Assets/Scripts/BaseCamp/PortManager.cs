using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PortManager : MonoBehaviour
{
    static PortManager instance;
    public static PortManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    [SerializeField] PortDatas allyPortDatas;
    [SerializeField] TMP_Text barrierStrength;
    public GameObject rewardSoldierPanel, rewardMutantPanel, activeBtn;

    public Port_State portState;

    public string rewardSoldierCode, rewardMutantCode;

    public PortData originPort;

    void Awake()
    {
        instance = this;
        barrierStrength.text = allyPortDatas.curBarrierStrength + "/" + allyPortDatas.maxBarrierStrength;
    }

    public IEnumerator SetSoldierCoroutine()
    {
        portState = Port_State.SetSoldier;
        if (activeBtn != null) { activeBtn.SetActive(false); }
        SetSoldierPortImg();
        rewardSoldierPanel.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.Escape) && portState == Port_State.SetSoldier)
        {
            yield return null;
        }
        portState = Port_State.Idle;
        allyPortDatas.curBarrierStrength += SaveManager.Instance.dataSheet.soldierDataSheet[PortManager.Instance.rewardSoldierCode].needBarrier;
        barrierStrength.text = allyPortDatas.curBarrierStrength + "/" + allyPortDatas.maxBarrierStrength;
        ReturnPortImg();
        rewardSoldierPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public IEnumerator SetMutantCoroutine()
    {
        portState = Port_State.SetMutant;
        if(activeBtn != null) { activeBtn.SetActive(false); }
        SetMutantPortImg();
        rewardMutantPanel.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.Escape) && portState == Port_State.SetMutant)
        {
            yield return null;
        }
        portState = Port_State.Idle;
        ReturnPortImg();
        rewardMutantPanel.SetActive(false);
        gameObject.SetActive(false);
    }
    //가능하면 초록색 불가능하면 빨간색
    public void SetSoldierPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock && allyPortDatas.portDatas[i].soldierCode == null)//언락이 되어 있고 솔져 코드가 없다면
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else if (!allyPortDatas.portDatas[i].unlock || allyPortDatas.portDatas[i].soldierCode != null)//언락 안 되어 있거나 이미 솔져 코드가 있다면
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0.7f, 0, 0);
            }
        }
    }

    public void SetMutantPortImg()
    {
        for(int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if(allyPortDatas.portDatas[i].soldierCode == rewardSoldierCode)//현재 포트의 솔져코드와 보상 솔져코드가 같다면
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else//그 외
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0.7f, 0, 0);
            }
        }
    }

    public void DragPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else if (!allyPortDatas.portDatas[i].unlock)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0.7f, 0, 0);
            }
        }
        originPort.portImg.color = new Color(1, 1, 1);
    }

    public void ReturnPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock) { allyPortDatas.portDatas[i].portImg.color = new Color(1, 1, 1); }//포트가 해제되어있다면 흰색
            else { allyPortDatas.portDatas[i].portImg.color = new Color(0.3f, 0.3f, 0.3f); }//아니라면 회색
        }
    }

    public void ControllActiveBtn(GameObject gameObject)
    {
        if (activeBtn != null) { activeBtn.SetActive(false); }
        activeBtn = gameObject;
        gameObject.SetActive(true);
    }
}
