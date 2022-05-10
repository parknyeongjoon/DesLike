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

    GameObject[] RelicsInstant;
    EventManager eventManager;
    public TMP_Text[] Prices = new TMP_Text[6];
    Dictionary<string, GameObject> RelicDic = new Dictionary<string, GameObject>();

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
            RelicReroll:
                randRelic[i] = Random.Range(0, relicLevelCount[i]);
                for (int j = 0; j < relicList.Count; j++)
                {
                    // �̹� ������ �ִ� �����̶�� �ٽ� ������
                    if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[0]].relicData.code)
                        goto RelicReroll;
                    // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
                }
                if (i < 4) relicPrice[i] = Random.Range(90, 21);   // 90~110G
                else relicPrice[i] = Random.Range(160, 21); // 160~180G

                relicList.Add(eventNode.ableRelicRewards[randRelic[i]]);
                RelicsInstant[i] = Instantiate(RelicPrefab, new Vector2(200 * i - 400, 200 - 120 * i), Quaternion.identity);
                RelicsInstant[i].GetComponent<Image>().sprite = relicList[i].relicData.relicImg;
                RelicsInstant[i].GetComponentInChildren<GameObject>().GetComponentInChildren<TMP_Text>().text = relicList[i].relicData.toopTip;
                

                switch (i)
                {
                    case 1:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck1);
                        break;
                    case 2:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck2);
                        break;
                    case 3:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck2);
                        break;
                    case 4:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck2);
                        break;
                    case 5:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck2);
                        break;
                    case 6:
                        RelicsInstant[i].GetComponent<Button>().onClick.AddListener(OpenCheck2);
                        break;
                }

                RelicImage[i].sprite = relicList[i].relicData.relicImg;
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
        vilShopNode.curRelic = relicList[0];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck2()
    {
        vilShopNode.curRelic = relicList[1];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        vilShopNode.curRelic = relicList[2];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);

    }

    public void OpenCheck4()
    {
        vilShopNode.curRelic = relicList[3];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        vilShopNode.curRelic = relicList[4];
        // relic instantiate �� �����ֱ�
        CheckPanel.SetActive(true);

    }

    public void OpenCheck6()
    {
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
