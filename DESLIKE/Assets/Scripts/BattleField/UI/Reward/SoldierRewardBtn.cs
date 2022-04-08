using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SoldierRewardBtn : MonoBehaviour
{
    [SerializeField]
    GameObject PortCanvas, btnPanel, soldierRewardPanel;

    public SoldierReward soldierReward;

    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = soldierReward.soldier.name.ToString();
        transform.GetChild(1).GetComponent<Image>().sprite = soldierReward.soldier.sprite;
    }

    public void GetReward()//뮤턴트 있는 경우도 생각
    {
       if(soldierReward != null)
        {
            if (SaveManager.Instance.allyPortDatas.activeSoldierList.ContainsKey(soldierReward.soldier.code))//이미 병사를 가지고 있다면 포트 한 칸 더 생성하기와 특화 중에 하나 선택하기
            {
                btnPanel.SetActive(true);
            }
            else//안 갖고 있다면 바로 포트에 생성하기
            {
                GetSoldier();
                //SoldierManager.GetSoldier(soldierCode, null);
            }
        }
    }

    public void GetSoldier()
    {
        Instantiate(PortCanvas);
        PortManager.Instance.soldierReward = soldierReward;
        PortManager.Instance.StartCoroutine(PortManager.Instance.SetSoldierCoroutine());
        Destroy(transform.parent.gameObject);
    }

    public void GetMutant()
    {
        //mutant 값 설정해주기
        Destroy(transform.parent.gameObject);
    }
}
