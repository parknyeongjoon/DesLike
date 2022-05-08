using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventShop : EventBasic
{
    public GameObject CheckPanel, ErrorPanel;
    public GameObject[] SoldOutPanel = new GameObject[6];
    public VilShopNode vilShopNode;
    public List<Relic> relicList;
    EventManager eventManager;
    public TMP_Text[] Prices = new TMP_Text[6];

    
    void OnEnable()
    {
        DataUpdate();
        ShopSetting();
        SoldOutPanelUpdate();
        isNewSet = false;
        eventEnd = false;
        SaveData();
    }

    void DataUpdate()
    {
        relicList = new List<Relic>();
        relicLevelCount[0] = eventNode.relicLevelCount[0];
        relicLevelCount[1] = relicLevelCount[0] + eventNode.relicLevelCount[1];
        relicLevelCount[2] = relicLevelCount[1] + eventNode.relicLevelCount[2];
    }

    void SoldOutPanelUpdate()
    {
        for (int i = 0; i < 6; i++)
        {
            if (isSoldOut[i])
                SoldOutPanel[i].SetActive(true);
            else
                SoldOutPanel[i].SetActive(false);
        }
    }

    void ShopSetting()
    {
    // randRelic[i] = Random.Range(0, relicLevelCount[0]); 일반용
    // randRelic[i] = Random.Range(relicLevelCount[0], relicLevelCount[1]); 희귀용
    // randRelic[i] = Random.Range(relicLevelCount[1], relicLevelCount[2]); 전설용

    // 1. 일반 유물 90G~110G
    firstRelic:
        if (isEventSet == false)
        {
            randRelic[0] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[0]].relicData.code)
                    goto firstRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[0] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[0]]);

    // 2. 일반 유물 90G~110G
    secondRelic:
        if (isEventSet == false)
        {
            randRelic[1] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[1]].relicData.code)
                    goto secondRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[1] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[1]]);

    // 3. 일반 유물 90G~110G
    thirdRelic:
        if (isEventSet == false)
        {
            randRelic[2] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[2]].relicData.code)
                    goto thirdRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[2] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[2]]);

    // 4. 일반 유물 90G~110G
    fourthRelic:
        if (isEventSet == false)
        {
            randRelic[3] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[3]].relicData.code)
                    goto fourthRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[3] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[3]]);

    // 5. 희귀 유물 160G~180G
    fifthRelic:
        if (isEventSet == false)
        {
            randRelic[4] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[4]].relicData.code)
                    goto fifthRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[4] = Random.Range(160, 21);   // 160~180G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[4]]);

    // 6. 희귀 유물 160G~180G
    sixthRelic:
        if (isEventSet == false)
        {
            randRelic[5] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // 이미 가지고 있는 유물이라면 다시 랜덤값
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[5]].relicData.code)
                    goto sixthRelic;
                // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
            }
            relicPrice[5] = Random.Range(160, 21);   // 160~180G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[5]]);

        for (int i = 0; i < 6; i++)
        {
            Prices[i].text = relicPrice[i] + "골드";
        }
    }

    public void OpenCheck1()
    {
        vilShopNode.curRelic = relicList[0];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);
    }

    public void OpenCheck2()
    {
        vilShopNode.curRelic = relicList[1];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        vilShopNode.curRelic = relicList[2];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);

    }

    public void OpenCheck4()
    {
        vilShopNode.curRelic = relicList[3];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        vilShopNode.curRelic = relicList[4];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);

    }

    public void OpenCheck6()
    {
        vilShopNode.curRelic = relicList[5];
        // relic instantiate 후 보여주기
        CheckPanel.SetActive(true);
    }

    public void CloseCheckPanel()
    {
        CheckPanel.SetActive(false);
    }

    public void BuyRelic()
    {
        if (gold < relicPrice[vilShopNode.curNumber])
        {
            Debug.Log("골드가 부족합니다.");
            ErrorPanel.SetActive(true); // 골드가 부족하다는 표시 후 클릭하면 없어지게끔
        }
        else
        {
            vilShopNode.GetRelic();
            gold -= relicPrice[vilShopNode.curNumber];
            isSoldOut[vilShopNode.curNumber] = true;
            SoldOutPanel[vilShopNode.curNumber].SetActive(true);    // 매진 표시
            SoldOutPanelUpdate();
        }
    }

    public void ErrorClose()
    {
        ErrorPanel.SetActive(false);
        CheckPanel.SetActive(false);
    }

    public void EndShop()
    {
        for (int i = 0; i < 6; i++)
            isSoldOut[i] = false;
        EndEvent();
    }
}
