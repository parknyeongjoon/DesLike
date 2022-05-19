using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventShop : EventBasic
{
    int curGold, curRelicCount;
    int[] relicLevelCount = new int[3];
    int[] randRelic = new int[6];   // 목록별 랜덤넘버
    int[] relicPrice = new int[6];  // 목록별 가격
    bool isNewSet, isEventSet;
    bool[] isSoldOut = new bool[6];


    List<Relic> relicList;
    List<Relic> curRelicList;

    [SerializeField] GameObject CheckPanel, ErrorPanel;
    [SerializeField] GameObject[] SoldOutPanel = new GameObject[6];
    [SerializeField] VilShopNode vilShopNode;
    [SerializeField] GameObject RelicPrefab;
    [SerializeField] Canvas RelicCanvas;
    [SerializeField] TMP_Text[] Prices = new TMP_Text[6];
    
    void OnEnable()
    {
        LoadData();
        DataUpdate();
        ShopSetting();
        SoldOutPanelUpdate();
        isNewSet = false;
        eventEnd = false;
        SaveData();
    }

    void SaveData()
    {
        SaveShopEData();
        SaveComData();
        saveManager.SaveGameData();
    }

    void SaveShopEData()
    {
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.isEventSet = isEventSet;

        for (int i = 0; i < 3; i++)
        {
            saveManager.gameData.villageData.randRelic[i] = randRelic[i];
            saveManager.gameData.villageData.relicPrice[i] = relicPrice[i];
            saveManager.gameData.villageData.isSoldOut[i] = isSoldOut[i];
        }
    }

    void LoadData()
    {
        LoadShopEData();
        LoadComData();
    }

    void LoadShopEData()
    {
        curGold = saveManager.gameData.goodsSaveData.gold;
        curRelicCount = RelicManager.instance.relicList.Count;
        isEventSet = saveManager.gameData.eventData.isEventSet;
        
        for (int i = 0; i < 3; i++)
        {
            randRelic[i] = saveManager.gameData.villageData.randRelic[i];
            relicPrice[i] = saveManager.gameData.villageData.relicPrice[i];
            isSoldOut[i] = saveManager.gameData.villageData.isSoldOut[i];
        }
        curRelicList = new List<Relic>();
        for (int i = 0; i < curRelicCount; i++)
            curRelicList.Add(RelicManager.instance.relicList[i]);
    }

    void DataUpdate()
    {
        relicLevelCount[0] = eventNode.relicLevelCount[0];
        relicLevelCount[1] = eventNode.relicLevelCount[1];
        relicLevelCount[2] = eventNode.relicLevelCount[2];
        relicList = RelicManager.instance.relicList;
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
        // randRelic[i] = relicLevelCount[0] + Random.Range(0, relicLevelCount[1]); 희귀용
        // randRelic[i] = relicLevelCount[0] + relicLevelCount[1] + Random.Range(0, relicLevelCount[2]); 전설용
        InfiniteLoopDetector.Run();

        if (isEventSet == false)
        {
            relicList = new List<Relic>(6);
            for (int i = 0; i < 6; i++)
            {
            RelicReroll:
                InfiniteLoopDetector.Run();
                if (i < 4)  // 일반 유물
                {
                    Debug.Log("1 : " + i);
                    relicPrice[i] = 90 + Random.Range(0, 21);   // 90~110G
                    randRelic[i] = Random.Range(0, relicLevelCount[0]); // 일반
                }
                else // 희귀 유물
                {
                    relicPrice[i] = 160 + Random.Range(0, 21); // 160~180G
                    randRelic[i] = relicLevelCount[0] + Random.Range(0, relicLevelCount[1]); // 희귀
                }
            
                for (int j = 0; j < i - 1; j++) // 이전 랜덤값과 같다면 재시도
                {
                    if (randRelic[i] == randRelic[j])
                        goto RelicReroll;
                 }
                for (int j = 0; j < curRelicCount; j++)
                {
                    InfiniteLoopDetector.Run();
                    if (curRelicList[j].relicData.code == eventNode.ableRelicRewards[randRelic[i]].relicData.code)
                        goto RelicReroll;
                }

                relicList.Add(eventNode.ableRelicRewards[randRelic[i]]);
          
                Prices[i].text = relicPrice[i] + "골드";
                Instantiate(eventNode.ableRelicRewards[randRelic[i]], RelicCanvas.transform.GetChild(i).transform);

                Debug.Log("for end : " + i);
            }
        }
        else
        {
            for(int i = 0; i<6; i++)
            {
                relicList.Add(eventNode.ableRelicRewards[randRelic[i]]);
                Prices[i].text = relicPrice[i] + "골드";
                Instantiate(eventNode.ableRelicRewards[randRelic[i]], RelicCanvas.transform.GetChild(i).transform);
            }
        }
    }

    public void OpenCheck(int i)
    {
        switch(i)
        {
            case 1:
                OpenCheck1();
                break;
            case 2:
                OpenCheck2();
                break;
            case 3:
                OpenCheck3();
                break;
            case 4:
                OpenCheck4();
                break;
            case 5:
                OpenCheck5();
                break;
            case 6:
                OpenCheck6();
                break;
        }
    }

    public void OpenCheck1()
    {
        vilShopNode.curRelic = relicList[0];
        vilShopNode.curNumber = 0;
        CheckPanel.SetActive(true);
        Instantiate(relicList[0], CheckPanel.transform.GetChild(0).transform);
    }

    public void OpenCheck2()
    {
        vilShopNode.curRelic = relicList[1];
        vilShopNode.curNumber = 1;
        Instantiate(relicList[1], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        vilShopNode.curRelic = relicList[2];
        vilShopNode.curNumber = 2;
        Instantiate(relicList[2], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck4()
    {
        vilShopNode.curRelic = relicList[3];
        vilShopNode.curNumber = 3;
        Instantiate(relicList[3], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        vilShopNode.curRelic = relicList[4];
        vilShopNode.curNumber = 4;
        Instantiate(relicList[4], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck6()
    {
        vilShopNode.curRelic = relicList[5];
        vilShopNode.curNumber = 5;
        Instantiate(relicList[5], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void CloseCheckPanel()
    {
        Debug.Log(vilShopNode.curRelic.relicData.relicName);
        Transform cloneTransform = CheckPanel.transform.GetChild(0).transform.Find(vilShopNode.curRelic.relicData.relicName + "(Clone)");
        if (cloneTransform != null)
            Destroy(cloneTransform.gameObject);
        else Debug.Log("파괴할 오브젝트가 없습니다.");
        CheckPanel.SetActive(false);
    }

    public void BuyRelic()
    {
        if (curGold < relicPrice[vilShopNode.curNumber])
        {
            ErrorPanel.SetActive(true); // 골드가 부족하다는 표시 후 클릭하면 없어지게끔
        }
        else
        {
            vilShopNode.GetRelic();
            curGold -= relicPrice[vilShopNode.curNumber];
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
        curDay += 2;
        SaveData();
        EndEvent();
    }
}
