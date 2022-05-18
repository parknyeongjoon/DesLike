using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventShop : EventBasic
{
    public GameObject CheckPanel, ErrorPanel;
    public GameObject[] SoldOutPanel = new GameObject[6];
    public VilShopNode vilShopNode;
    public GameObject RelicSpawner;
    public GameObject RelicPrefab;
    public Canvas RelicCanvas;
    List<Relic> relicList;
   
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
        // randRelic[i] = Random.Range(0, relicLevelCount[0]); �Ϲݿ�
        // randRelic[i] = relicLevelCount[0] + Random.Range(0, relicLevelCount[1]); ��Ϳ�
        // randRelic[i] = relicLevelCount[0] + relicLevelCount[1] + Random.Range(0, relicLevelCount[2]); ������
        InfiniteLoopDetector.Run();

        if (isEventSet == false)
        {
            relicList = new List<Relic>(6);
            for (int i = 0; i < 6; i++)
            {
            RelicReroll:
                InfiniteLoopDetector.Run();
                if (i < 4)  // �Ϲ� ����
                {
                    Debug.Log("1 : " + i);
                    relicPrice[i] = 90 + Random.Range(0, 21);   // 90~110G
                    randRelic[i] = Random.Range(0, relicLevelCount[0]); // �Ϲ�
                }
                else // ��� ����
                {
                    relicPrice[i] = 160 + Random.Range(0, 21); // 160~180G
                    randRelic[i] = relicLevelCount[0] + Random.Range(0, relicLevelCount[1]); // ���
                }
            
                for (int j = 0; j < i - 1; j++) // ���� �������� ���ٸ� ��õ�
                {
                    if (randRelic[i] == randRelic[j])
                        goto RelicReroll;
                }

                for (int j = 0; j < i - 1; j++)
                {
                    InfiniteLoopDetector.Run();
                    // �̹� ������ �ִ� �����̶�� �ٽ� ������
                    if (relicList[j].relicData.code == eventNode.ableRelicRewards[randRelic[i]].relicData.code)
                        goto RelicReroll;
                    // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
                }
                
                relicList.Add(eventNode.ableRelicRewards[randRelic[i]]);
          
                Prices[i].text = relicPrice[i] + "���";
                Debug.Log("for end : " + i);
            }
            for (int i = 0; i < 6; i++)
                Instantiate(eventNode.ableRelicRewards[randRelic[i]], RelicCanvas.transform.GetChild(i).transform);
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
        
        CheckPanel.SetActive(true);
        Instantiate(relicList[0], CheckPanel.transform.GetChild(0).transform);
    }

    public void OpenCheck2()
    {
        Debug.Log("OpenCheck2");
        vilShopNode.curRelic = relicList[1];
        // relic instantiate �� �����ֱ�
        Instantiate(relicList[1], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck3()
    {
        Debug.Log("OpenCheck3");
        vilShopNode.curRelic = relicList[2];
        // relic instantiate �� �����ֱ�
        Instantiate(relicList[2], CheckPanel.transform.GetChild(0).transform);

        CheckPanel.SetActive(true);
    }

    public void OpenCheck4()
    {
        Debug.Log("OpenCheck4");
        vilShopNode.curRelic = relicList[3];
        // relic instantiate �� �����ֱ�
        Instantiate(relicList[3], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);
    }

    public void OpenCheck5()
    {
        Debug.Log("OpenCheck5");
        vilShopNode.curRelic = relicList[4];
        // relic instantiate �� �����ֱ�
        Instantiate(relicList[4], CheckPanel.transform.GetChild(0).transform);
        CheckPanel.SetActive(true);

    }

    public void OpenCheck6()
    {
        Debug.Log("OpenCheck6");
        vilShopNode.curRelic = relicList[5];
        // relic instantiate �� �����ֱ�
        Instantiate(relicList[5], CheckPanel.transform.GetChild(0).transform);

        CheckPanel.SetActive(true);
    }

    public void CloseCheckPanel()
    {
        Debug.Log(vilShopNode.curRelic.relicData.relicName);
        Transform cloneTransform = CheckPanel.transform.GetChild(0).transform.Find(vilShopNode.curRelic.relicData.relicName + "(Clone)");
        if (cloneTransform != null)
            Destroy(cloneTransform.gameObject);
        else Debug.Log("�ı��� ������Ʈ�� �����ϴ�.");
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
