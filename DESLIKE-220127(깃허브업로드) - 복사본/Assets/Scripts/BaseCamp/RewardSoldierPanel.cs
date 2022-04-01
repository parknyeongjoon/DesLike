using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardSoldierPanel : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image img;

    void OnEnable()
    {
        text.text = SaveManager.Instance.dataSheet.soldierDataSheet[PortManager.Instance.rewardSoldierCode].needBarrier.ToString();
        img.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[PortManager.Instance.rewardSoldierCode].sprite;
    }
}
