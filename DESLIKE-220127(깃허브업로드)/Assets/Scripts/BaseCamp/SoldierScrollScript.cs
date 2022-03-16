using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierScrollScript : MonoBehaviour
{
    ScrollRect scrollRect;

    [SerializeField] GameObject portPrefab;
    [SerializeField] SoldierPrefabScript prefabScript;

    [SerializeField]PortDatas allyPortDatas;

    public int xSize, xSpace;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        //allyPortDatas = SaveManager.Instance.gameData.allyPortDatas;���� �ø�������� ����� �̰� �ֱ�
    }

    void OnEnable()
    {
        SetScroll();
    }

    void SetScroll()//��ũ�� �ȿ� ��� ������Ʈ ���� ���� ��ũ���� ������ �������ִ� �Լ�
    {
        int count = 0;

        //�����
        for(int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if(allyPortDatas.portDatas[i].soldierCode == "")
            allyPortDatas.portDatas[i].soldierCode = null;
        }

        foreach(var soldierData in allyPortDatas.activeSoldierList.Values)
        {
            count++;
            prefabScript.soldierCode = soldierData.code;
            Instantiate(portPrefab, scrollRect.content);
        }
        scrollRect.content.sizeDelta = new Vector2((xSize + xSpace) * count, scrollRect.content.sizeDelta.y);
    }
}
