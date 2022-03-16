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

    public Coroutine setPortCoroutine;
    bool isSet = false;

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

        //지우기
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

    public IEnumerator SetPortCoroutine(string soldierCode)
    {
        isSet = true;
        SetPortImg();
        while (!Input.GetKeyDown(KeyCode.Escape) && isSet)
        {
            Debug.Log("0");
            yield return null;
        }
        Debug.Log("1");
        ReturnPortImg();
    }

    public void SetPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            if (allyPortDatas.portDatas[i].unlock && allyPortDatas.portDatas[i].soldierCode == null)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0, 0.7f, 0);
            }
            else if (!allyPortDatas.portDatas[i].unlock || allyPortDatas.portDatas[i].soldierCode != null)
            {
                allyPortDatas.portDatas[i].portImg.color = new Color(0.7f, 0, 0);
            }
        }
    }

    public void ReturnPortImg()
    {
        for (int i = 0; i < allyPortDatas.portDatas.Length; i++)
        {
            allyPortDatas.portDatas[i].portImg.color = new Color(1, 1, 1);
        }
    }
}
