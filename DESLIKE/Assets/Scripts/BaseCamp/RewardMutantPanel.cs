using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardMutantPanel : MonoBehaviour
{
    [SerializeField] Image originMutantImg, changeMuantImg;
    [SerializeField] GameObject toolTipPanel;
    [SerializeField] TMP_Text toolTipText;

    void OnEnable()
    {
        SetMutantImg();
    }

    public void SetMutantImg()
    {
        if(PortManager.Instance.originPort && PortManager.Instance.originPort.mutantCode != "")
        {
            originMutantImg.sprite = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.originPort.mutantCode].mutantImg;
            originMutantImg.gameObject.SetActive(true);
        }
        else
        {
            originMutantImg.gameObject.SetActive(false);
        }
        changeMuantImg.sprite = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.rewardMutantCode].mutantImg;
    }

    public void OkBtn()//Ok��ư�� �Ҵ�
    {
        PortManager.Instance.originPort.mutantCode = PortManager.Instance.rewardMutantCode;
        PortManager.Instance.portState = Port_State.Idle;
    }

    public void SeeOriginTooltip()
    {
        toolTipText.text = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.originPort.mutantCode].toolTip;
        toolTipPanel.SetActive(true);
    }

    public void SeeChangeTooltip()
    {
        toolTipText.text = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.rewardMutantCode].toolTip;
        toolTipPanel.SetActive(true);
    }

    public void TooltipPanelOff()
    {
        toolTipPanel.SetActive(false);
    }

    public void RerollBtn()
    {
        //�ڵ� �ٲٰ�
        //�̹��� ����
    }
}
