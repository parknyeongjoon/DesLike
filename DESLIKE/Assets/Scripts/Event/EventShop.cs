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
    // randRelic[i] = Random.Range(0, relicLevelCount[0]); �Ϲݿ�
    // randRelic[i] = Random.Range(relicLevelCount[0], relicLevelCount[1]); ��Ϳ�
    // randRelic[i] = Random.Range(relicLevelCount[1], relicLevelCount[2]); ������

    // 1. �Ϲ� ���� 90G~110G
    firstRelic:
        if (isEventSet == false)
        {
            randRelic[0] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[0]].relicData.code)
                    goto firstRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[0] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[0]]);

    // 2. �Ϲ� ���� 90G~110G
    secondRelic:
        if (isEventSet == false)
        {
            randRelic[1] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[1]].relicData.code)
                    goto secondRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[1] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[1]]);

    // 3. �Ϲ� ���� 90G~110G
    thirdRelic:
        if (isEventSet == false)
        {
            randRelic[2] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[2]].relicData.code)
                    goto thirdRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[2] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[2]]);

    // 4. �Ϲ� ���� 90G~110G
    fourthRelic:
        if (isEventSet == false)
        {
            randRelic[3] = Random.Range(0, relicLevelCount[0]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[3]].relicData.code)
                    goto fourthRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[3] = Random.Range(90, 21);   // 90~110G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[3]]);

    // 5. ��� ���� 160G~180G
    fifthRelic:
        if (isEventSet == false)
        {
            randRelic[4] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[4]].relicData.code)
                    goto fifthRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[4] = Random.Range(160, 21);   // 160~180G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[4]]);

    // 6. ��� ���� 160G~180G
    sixthRelic:
        if (isEventSet == false)
        {
            randRelic[5] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
            for (int i = 0; i < relicList.Count; i++)
            {
                // �̹� ������ �ִ� �����̶�� �ٽ� ������
                if (relicList[i].relicData.code == eventNode.ableRelicRewards[randRelic[5]].relicData.code)
                    goto sixthRelic;
                // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
            }
            relicPrice[5] = Random.Range(160, 21);   // 160~180G
        }
        relicList.Add(eventNode.ableRelicRewards[randRelic[5]]);

        for (int i = 0; i < 6; i++)
        {
            Prices[i].text = relicPrice[i] + "���";
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
