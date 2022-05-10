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

    GameObject[] RelicsInstant;
    EventManager eventManager;
    public TMP_Text[] Prices = new TMP_Text[6];
    Dictionary<string, GameObject> RelicDic = new Dictionary<string, GameObject>();
    RelicData relicData;
    Relic[] relicScript = new Relic[6];



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
        relicLevelCount[1] = relicLevelCount[0] + eventNode.relicLevelCount[1];
        relicLevelCount[2] = relicLevelCount[1] + eventNode.relicLevelCount[2];
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
        // randRelic[i] = Random.Range(0, relicLevelCount[0]); �Ϲݿ�
        // randRelic[i] = Random.Range(relicLevelCount[0], relicLevelCount[1]); ��Ϳ�
        // randRelic[i] = Random.Range(relicLevelCount[1], relicLevelCount[2]); ������

        if (isEventSet == false)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i < 4)
                {
                    relicPrice[i] = 90 + Random.Range(0, 21);   // 90~110G
                    randRelic[i] = Random.Range(0, relicLevelCount[0]); // �Ϲ�
                }
                else
                {
                    relicPrice[i] = 160 + Random.Range(0, 21); // 160~180G
                    randRelic[i] = Random.Range(relicLevelCount[0], relicLevelCount[1]); // ���
                }

            RelicReroll:
                InfiniteLoopDetector.Run();
                for (int j = 0; j < i - 1; j++)
                {
                    if (relicList[i].relicData.code == relicList[j].relicData.code)
                        goto RelicReroll;
                }

                for (int j = 0; j < relicList.Count; j++)
                {
                    // �̹� ������ �ִ� �����̶�� �ٽ� ������
                    if (relicList[j].relicData.code == eventNode.ableRelicRewards[randRelic[i]].relicData.code)
                        goto RelicReroll;
                    // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
                }
                
                relicScript[i] = eventNode.ableRelicRewards[randRelic[i]].GetComponent<Relic>();
                relicData = relicScript[i].relicData;
                relicScript[i].transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = relicData.relicName.ToString();
                relicScript[i].transform.GetComponent<Image>().sprite = relicData.relicImg;
                
                Instantiate(relicScript[i], RelicCanvas.transform.GetChild(i).transform);

                switch (i)  // instantiate�� object�� button�߰��ϴ� �� �˾ƺ���
                {
                    case 1:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck1(); });
                        break;
                    case 2:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck2(); });
                        break;
                    case 3:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck3(); });
                        break;
                    case 4:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck4(); });
                        break;
                    case 5:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck5(); });
                        break;
                    case 6:
                        relicScript[i].GetComponent<Button>().onClick.AddListener(delegate { OpenCheck6(); });
                        break;
                }
                Prices[i].text = relicPrice[i] + "���";
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
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck2()
    {
        Debug.Log("OpenCheck2");
        vilShopNode.curRelic = relicList[1];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        Debug.Log("OpenCheck3");
        vilShopNode.curRelic = relicList[2];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);

    }

    public void OpenCheck4()
    {
        Debug.Log("OpenCheck4");
        vilShopNode.curRelic = relicList[3];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        Debug.Log("OpenCheck5");
        vilShopNode.curRelic = relicList[4];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);

    }

    public void OpenCheck6()
    {
        Debug.Log("OpenCheck6");
        vilShopNode.curRelic = relicList[5];
        // relic instantiate �� �����ֱ�
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
            Debug.Log("��尡 �����մϴ�.");
            ErrorPanel.SetActive(true); // ��尡 �����ϴٴ� ǥ�� �� Ŭ���ϸ� �������Բ�
        }
        else
        {
            vilShopNode.GetRelic();
            gold -= relicPrice[vilShopNode.curNumber];
            isSoldOut[vilShopNode.curNumber] = true;
            SoldOutPanel[vilShopNode.curNumber].SetActive(true);    // ���� ǥ��
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
