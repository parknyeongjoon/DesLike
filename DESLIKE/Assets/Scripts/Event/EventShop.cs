using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventShop : EventBasic
{
    public GameObject CheckPanel, ErrorPanel;
    public Image[] RelicImage = new Image[6];
    public GameObject[] SoldOutPanel = new GameObject[6];
    public VilShopNode vilShopNode;
    public List<Relic> relicList;
    public GameObject RelicSpawner;
    public GameObject RelicPrefab;
    public Canvas RelicCanvas;
    List<Relic> RelicsInShop;

    GameObject[] RelicsInstant;
    EventManager eventManager;
    public TMP_Text[] Prices = new TMP_Text[6];
    Dictionary<string, GameObject> RelicDic = new Dictionary<string, GameObject>();
    RelicData relicData;
    // Relic[] relicScript = new Relic[6];
    Relic checkRelic;



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

        if (isEventSet == false)
        {
            for (int i = 0; i < 6; i++)
            {
                Debug.Log("for : " + i);
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
                Debug.Log("2 : " + i);

                for (int j = 0; j < i - 1; j++)
                {
                    if (randRelic[i] == randRelic[j])
                        goto RelicReroll;
                }

                Debug.Log("3 : " + i);

                for (int j = 0; j < relicList.Count; j++)
                {
                    // 이미 가지고 있는 유물이라면 다시 랜덤값
                    if (relicList[j].relicData.code == eventNode.ableRelicRewards[randRelic[i]].relicData.code)
                        goto RelicReroll;
                    // 획득 유물 갯수가 한정되어있어서 모든 유물을 가진 경우의 수는 제외함
                }
                Debug.Log(i + " : " + randRelic[i]);

                RelicsInShop.Add(eventNode.ableRelicRewards[randRelic[i]]);
                Instantiate(RelicsInShop[i], RelicCanvas.transform.GetChild(i).transform);

                switch (i)  // instantiate한 object에 button추가하는 법 알아보기
                {
                    case 1:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck1(); });
                        break;
                    case 2:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck2(); });
                        break;
                    case 3:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck3(); });
                        break;
                    case 4:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck4(); });
                        break;
                    case 5:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck5(); });
                        break;
                    case 6:
                        RelicsInShop[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck6(); });
                        break;
                }
                Prices[i].text = relicPrice[i] + "골드";
                Debug.Log("for end : " + i);
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
        Debug.Log("OpenCheck1");
        vilShopNode.curRelic = relicList[0];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[0], CheckPanel.transform.GetChild(0).transform);

        CheckPanel.SetActive(true);
    }

    public void OpenCheck2()
    {
        Debug.Log("OpenCheck2");
        vilShopNode.curRelic = relicList[1];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[1], CheckPanel.transform.GetChild(0).transform);

        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        Debug.Log("OpenCheck3");
        vilShopNode.curRelic = relicList[2];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[2], CheckPanel.transform.GetChild(0).transform);

        CheckPanel.SetActive(true);
    }

    public void OpenCheck4()
    {
        Debug.Log("OpenCheck4");
        vilShopNode.curRelic = relicList[3];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[3], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        Debug.Log("OpenCheck5");
        vilShopNode.curRelic = relicList[4];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[4], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);

    }

    public void OpenCheck6()
    {
        Debug.Log("OpenCheck6");
        vilShopNode.curRelic = relicList[5];
        // relic instantiate 후 보여주기
        Destroy(CheckPanel.transform.GetChild(0).transform.GetChild(0));
        Instantiate(RelicsInShop[5], CheckPanel.transform.GetChild(0).transform);

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
