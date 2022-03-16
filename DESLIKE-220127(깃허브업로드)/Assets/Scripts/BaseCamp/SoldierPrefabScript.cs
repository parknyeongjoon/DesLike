using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierPrefabScript : MonoBehaviour
{
    [SerializeField] Image prefabImg;
    public string soldierCode;

    void OnEnable()
    {
        prefabImg.sprite = SaveManager.Instance.dataSheet.soldierDataSheet[soldierCode].sprite;
    }

    public void Set_Port()//���� ��ư �������� ������ �� �ߺ����� �ʰ� �ڷ�ƾ�� �������ִ� �Լ�
    {
        if(PortManager.Instance.setPortCoroutine != null)
        {
            StopCoroutine(PortManager.Instance.setPortCoroutine);
        }
        PortManager.Instance.setPortCoroutine = StartCoroutine(PortManager.Instance.SetPortCoroutine());
        PortManager.Instance.soldierCode = soldierCode;
    }
}
