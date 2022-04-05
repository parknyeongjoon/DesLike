using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardMutantPanel : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image img;

    void OnEnable()
    {
        text.text = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.rewardMutantCode].toolTip;
        img.sprite = SaveManager.Instance.dataSheet.mutantDataSheet[PortManager.Instance.rewardMutantCode].mutantImg;
    }
}
