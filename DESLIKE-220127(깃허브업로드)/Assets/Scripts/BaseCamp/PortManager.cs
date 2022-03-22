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
    public GameObject rewardSoldierPanel, activeBtn;

    public Port_State portState;

    public string rewardSoldierCode;

    public PortData originPort;

    void Awake()
    {
        instance = this;
        barrierStrength.text = allyPortDatas.curBarrierStrength + "/" + allyPortDatas.maxBarrierStrength;
    }

    public IEnumerator SetPortCoroutine()
    {
        portState = Port_State.Set;
        if (activeBtn != null) { activeBtn.SetActive(false); }
        SetPortImg();
        rewardSoldierPanel.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.Escape) && portState == Port_State.Set)
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

    public void SetPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock && allyPortDatas.portDatas[i].soldierCode == null)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else if (!allyPortDatas.portDatas[i].unlock || allyPortDatas.portDatas[i].soldierCode != null)
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
