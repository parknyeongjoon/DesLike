using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DicManager : MonoBehaviour
{
    [SerializeField]
    GameObject dicBtn_Prefab;
    [SerializeField]
    DetailPanelScript detailPanelScript;

    void Start()
    {
        foreach (SoldierData soldierData in SaveManager.Instance.dataSheet.soldierDataSheet.Values)
        {
            GameObject create_dicBtn;
            create_dicBtn = Instantiate(dicBtn_Prefab, this.transform);

            DicBtnScript dicBtnScript;
            dicBtnScript = create_dicBtn.GetComponent<DicBtnScript>();
            dicBtnScript.soldierData = soldierData;
            dicBtnScript.GetComponent<Button>().onClick.AddListener(() => detailPanelScript.Set_DetailPanel(soldierData));
        }
    }
}