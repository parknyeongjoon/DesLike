using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierPrefabScript : MonoBehaviour
{
    [SerializeField] Image prefabImg;
    public string soldierCode;
    public SoldierScrollScript scrollScript;

    void OnEnable()
    {
        prefabImg.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[soldierCode].sprite;
    }

    public void Set_Port()//병사 버튼 프리팹을 눌렀을 때 중복되지 않게 코루틴을 실행해주는 함수
    {
        if(scrollScript.setPortCoroutine != null)
        {
            StopCoroutine(scrollScript.setPortCoroutine);
        }
        scrollScript.setPortCoroutine = StartCoroutine(scrollScript.SetPortCoroutine(soldierCode));
    }
}
