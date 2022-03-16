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

    public void Set_Port()//���� ��ư �������� ������ �� �ߺ����� �ʰ� �ڷ�ƾ�� �������ִ� �Լ�
    {
        if(scrollScript.setPortCoroutine != null)
        {
            StopCoroutine(scrollScript.setPortCoroutine);
        }
        scrollScript.setPortCoroutine = StartCoroutine(scrollScript.SetPortCoroutine(soldierCode));
    }
}
