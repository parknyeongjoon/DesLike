using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantPanelScript : MonoBehaviour
{
    [SerializeField]
    Image mutantImg;
    [SerializeField]
    Text mutantToolTip, rerollText;
    List<GameObject> mutantList;

    public string mutantCode;
    Mutant mutantScript;

    int remainReroll;

    void OnEnable()
    {
        remainReroll = 3;
        mutantList = SaveManager.Instance.dataSheet.mutantObjectSheet;
        SetMutant();
        SetMutantPanel();
    }

    void SetMutant()
    {
        int mutantIndex = Random.Range(0,mutantList.Count);
        /*  일단 주석처리 해놨으니 알아서 살려보십쇼
        mutant = mutantList[mutantIndex];
        mutantScript = mutant.GetComponent<Mutant>();
        */    
        Debug.Log("SetMutant");//애니메이션 효과 넣기
    }

    void SetMutantPanel()
    {
        mutantImg.sprite = mutantScript.mutantData.mutantImg;
        mutantToolTip.text = mutantScript.mutantData.toolTip;
        rerollText.text = "Reroll : " + remainReroll.ToString();
    }

    public void RerollMutant()
    {
        if (remainReroll > 0)
        {
            remainReroll--;
            SetMutant();
            SetMutantPanel();
        }
    }

    public void Close_Btn()
    {
        gameObject.SetActive(false);
    }
}
