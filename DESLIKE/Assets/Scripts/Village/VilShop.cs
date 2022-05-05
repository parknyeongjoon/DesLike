using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VilShop : MonoBehaviour
{
    public Map map;
    public GameObject ShopPanel, CheckPanel, ErrorPanel;
    public GameObject[] SoldOutPanel = new GameObject[6];
    public VilShopNode vilShopNode;
    VillageManager villagemanager;
    VillageNode villageNode;
    List<Relic> relicList;
    SaveManager saveManager;
    public TMP_Text[] Prices = new TMP_Text[6];
    int[] relicLevelCount = new int[3];
    int[] randRelic = new int[6];   // ��Ϻ� �����ѹ�
    int[] relicPrice = new int[6];  // ��Ϻ� ����
    bool[] isSoldOut = new bool[6];
    bool isNewSet;

    void OnEnable()
    {
        saveManager = SaveManager.Instance;
        DataUpdate();
        ShopSetting();
        isNewSet = false;
    }

    void DataUpdate()
    {
        relicList = new List<Relic>();
        villageNode = (VillageNode)map.curMapNode;
        relicLevelCount[0] = villageNode.relicLevelCount[0];
        relicLevelCount[1] = relicLevelCount[0] + villageNode.relicLevelCount[1];
        relicLevelCount[2] = relicLevelCount[1] + villageNode.relicLevelCount[2];
        isNewSet = saveManager.gameData.villageData.isNewSet;
        if (isNewSet == false)
        {
            for (int i = 0; i < 6; i++)
            {
                randRelic[i] = saveManager.gameData.villageData.randRelic[i];
                relicPrice[i] = saveManager.gameData.villageData.relicPrice[i];
                isSoldOut[i] = saveManager.gameData.villageData.isSoldOut[i];
            }
        }
    }

    void ShopSetting()
    {
        for(int i=0; i<6; i++)
        {
            if(isSoldOut[i])
                SoldOutPanel[i].SetActive(true);
            else
            SoldOutPanel[i].SetActive(false);
        }
        // randRelic[i] = Random.Range(0, relicLevelCount[0]); �Ϲݿ�
        // randRelic[i] = Random.Range(relicLevelCount[0], relicLevelCount[1]); ��Ϳ�
        // randRelic[i] = Random.Range(relicLevelCount[1], relicLevelCount[2]); ������

        // 1. �Ϲ� ���� 90G~110G
        firstRelic:
        randRelic[0] = Random.Range(0, relicLevelCount[0]);
        for (int i = 0; i<relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[0]].relicData.code)
                goto firstRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[0]]);
        relicPrice[0] = Random.Range(90, 21);   // 90~110G
        
        // 2. �Ϲ� ���� 90G~110G
        secondRelic:
        randRelic[1] = Random.Range(0, relicLevelCount[0]);
        for (int i = 0; i < relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[1]].relicData.code)
                goto secondRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[1]]);
        relicPrice[1] = Random.Range(90, 21);   // 90~110G
        
        // 3. �Ϲ� ���� 90G~110G
        thirdRelic:
        randRelic[2] = Random.Range(0, relicLevelCount[0]);
        for (int i = 0; i < relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[2]].relicData.code)
                goto thirdRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[2]]);
        relicPrice[2] = Random.Range(90, 21);   // 90~110G

        // 4. �Ϲ� ���� 90G~110G
        fourthRelic:
        randRelic[3] = Random.Range(0, relicLevelCount[0]);
        for (int i = 0; i < relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[3]].relicData.code)
                goto fourthRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[3]]);
        relicPrice[3] = Random.Range(90, 21);   // 90~110G

        // 5. ��� ���� 160G~180G
        fifthRelic:
        randRelic[4] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
        for (int i = 0; i < relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[4]].relicData.code)
                goto fifthRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[4]]);
        relicPrice[4] = Random.Range(160, 21);   // 160~180G

        // 6. ��� ���� 160G~180G
        sixthRelic:
        randRelic[5] = Random.Range(relicLevelCount[0], relicLevelCount[1]);
        for (int i = 0; i < relicList.Count; i++)
        {
            // �̹� ������ �ִ� �����̶�� �ٽ� ������
            if (relicList[i].relicData.code == villageNode.ableRelicRewards[randRelic[5]].relicData.code)
                goto sixthRelic;
            // ȹ�� ���� ������ �����Ǿ��־ ��� ������ ���� ����� ���� ������
        }
        relicList.Add(villageNode.ableRelicRewards[randRelic[5]]);
        relicPrice[5] = Random.Range(160, 21);   // 160~180G

        for(int i = 0; i<6; i++)
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
        if (saveManager.gameData.goodsSaveData.gold < relicPrice[vilShopNode.curNumber])
        {
            Debug.Log("��尡 �����մϴ�.");
            ErrorPanel.SetActive(true); // ��尡 �����ϴٴ� ǥ�� �� Ŭ���ϸ� �������Բ�
        }
        else
        {
            vilShopNode.GetRelic();
            saveManager.gameData.goodsSaveData.gold -= relicPrice[vilShopNode.curNumber];
            SoldOutPanel[vilShopNode.curNumber].SetActive(true);    // ���� ǥ��
        }
    }

    public void ErrorClose()
    {
        ErrorPanel.SetActive(false);
        CheckPanel.SetActive(false);
    }

    public void PanelOpenBtn()
    {
        ShopPanel.SetActive(true);
    }
    
    public void PanelCloseBtn()
    {
        ShopPanel.SetActive(false);
    }
}
