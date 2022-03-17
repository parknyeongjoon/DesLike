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
        //allyPortDatas = SaveManager.Instance.gameData.allyPortDatas;위에 시리얼라이즈 지우고 이거 넣기
    }

    void OnEnable()
    {
        SetScroll();
    }

    void SetScroll()//스크롤 안에 담긴 오브젝트 수에 따라 스크롤의 스케일 변경해주는 함수
    {
        int count = 0;
        foreach(var soldierData in allyPortDatas.activeSoldierList.Values)
        {
            count++;
            prefabScript.soldierCode = soldierData.code;
            Instantiate(portPrefab, scrollRect.content);
        }
        count = 10;//지우기
        scrollRect.content.sizeDelta = new Vector2((xSize + xSpace) * count + 60, scrollRect.content.sizeDelta.y);
    }
}
