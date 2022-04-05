using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMutantPanelScript : MonoBehaviour
{
    [SerializeField]
    Image originMutantImg, changeMutantImg;
    [SerializeField]
    Text originToolTip, changeToolTip;

    public SoldierData soldierData;
    public string changeMutant;
    Mutant mutantScript;
    Mutant changeMutantScript;

    void OnEnable()
    {
        SetChangeMutantPanel();
    }

    void SetChangeMutantPanel()
    {
        // mutantScript = soldierData.mutantCode.GetComponent<Mutant>();
        originMutantImg.sprite = mutantScript.mutantData.mutantImg;
        originToolTip.text = mutantScript.mutantData.toolTip;
        // changeMutantScript = changeMutant.GetComponent<Mutant>();
        changeMutantImg.sprite = changeMutantScript.mutantData.mutantImg;
        changeToolTip.text = changeMutantScript.mutantData.toolTip;
    }

    public void Close_Btn()
    {
        gameObject.SetActive(false);
    }
}
