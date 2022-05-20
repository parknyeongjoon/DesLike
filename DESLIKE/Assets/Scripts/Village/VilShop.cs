using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VilShop : MonoBehaviour
{
    int curGold, curRelicCount;
    int[] relicPrice = new int[6];  // 목록별 가격
    bool isNewSet, isEventSet;
    bool[] isSoldOut = new bool[6];
    string[] relicsInShop = new string[6];

    Dictionary<string, Relic> relicList;
    SaveManager saveManager;

    [SerializeField] Map map;
    [SerializeField] VillageNode villageNode;
    [SerializeField] Button EndButton;
    [SerializeField] GameObject ShopPanel, CheckPanel, ErrorPanel;
    [SerializeField] GameObject[] SoldOutPanel = new GameObject[6];
    [SerializeField] VilShopNode vilShopNode;
    [SerializeField] GameObject RelicPrefab;
    [SerializeField] Canvas RelicCanvas;
    [SerializeField] TMP_Text[] Prices = new TMP_Text[6];

    void OnEnable()
    {
        saveManager = SaveManager.Instance;

        LoadData();
        ShopSetting();
        SoldOutPanelUpdate();
        isNewSet = false;
        SaveData();
    }

    void SaveData()
    {
        saveManager.gameData.goodsSaveData.gold = curGold;
        saveManager.gameData.eventData.isEventSet = isEventSet;

        for (int i = 0; i < 6; i++)
        {
            saveManager.gameData.villageData.relicsInShop[i] = relicsInShop[i];
            saveManager.gameData.villageData.relicPrice[i] = relicPrice[i];
            saveManager.gameData.villageData.isSoldOut[i] = isSoldOut[i];
        }
        saveManager.SaveGameData();
    }

    void LoadData()
    {
        curGold = saveManager.gameData.goodsSaveData.gold;
        curRelicCount = RelicManager.instance.relicList.Count;
        isEventSet = saveManager.gameData.eventData.isEventSet;
        villageNode = (VillageNode)map.curMapNode;
        
        for (int i = 0; i < 6; i++)
        {
            relicsInShop[i] = saveManager.gameData.villageData.relicsInShop[i];
            relicPrice[i] = saveManager.gameData.villageData.relicPrice[i];
            isSoldOut[i] = saveManager.gameData.villageData.isSoldOut[i];
        }
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
        relicList = new Dictionary<string, Relic>();

        if (isEventSet == false)
        {
           
            for (int i = 0; i < 6; i++)
            {
            RelicReroll:
                InfiniteLoopDetector.Run();
                if (i < 4)  // 일반 유물
                {
                    relicPrice[i] = 90 + Random.Range(0, 21);   // 90~110G
                    relicsInShop[i] = villageNode.SetNorRel();
                }
                else // 희귀 유물
                {
                    relicPrice[i] = 160 + Random.Range(0, 21); // 160~180G
                    relicsInShop[i] = villageNode.SetEpicRel();
                }

                for (int j = 0; j < i - 1; j++) // 이전 랜덤값과 같다면 재시도
                {
                    if (relicList.ContainsKey(relicsInShop[i]))
                        goto RelicReroll;
                }

                if (saveManager.dataSheet.relicDataSheet[relicsInShop[i]])
                {
                    GameObject relicObject = saveManager.dataSheet.relicObjectSheet[relicsInShop[i]];
                    relicList.Add(relicsInShop[i], relicObject.GetComponent<Relic>());
                    Instantiate(relicObject, RelicCanvas.transform.GetChild(i).transform);
                }
                Prices[i].text = relicPrice[i] + "골드";
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject relicObject = saveManager.dataSheet.relicObjectSheet[relicsInShop[i]];
                relicList.Add(relicsInShop[i], relicObject.GetComponent<Relic>());
                Instantiate(relicObject, RelicCanvas.transform.GetChild(i).transform);
                Prices[i].text = relicPrice[i] + "골드";
            }
        }
    }

    public void OpenCheck(int i)
    {
        switch (i)
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

    void CheckPanelSet()
    {
        CheckPanel.SetActive(true);
        GameObject relicObject = saveManager.dataSheet.relicObjectSheet[relicsInShop[vilShopNode.curNumber]];
        Instantiate(relicObject, CheckPanel.transform.GetChild(0).transform);
    }

    public void OpenCheck1()
    {
        vilShopNode.curNumber = 0;
        CheckPanelSet();
    }

    public void OpenCheck2()
    {
        vilShopNode.curNumber = 1;
        CheckPanelSet();
    }

    public void OpenCheck3()
    {
        vilShopNode.curNumber = 2;
        CheckPanelSet();
    }

    public void OpenCheck4()
    {
        vilShopNode.curNumber = 3;
        CheckPanelSet();
    }

    public void OpenCheck5()
    {
        vilShopNode.curNumber = 4;
        CheckPanelSet();
    }

    public void OpenCheck6()
    {
        vilShopNode.curNumber = 5;
        CheckPanelSet();
    }

    public void CloseCheckPanel()
    {
        Transform cloneTransform = CheckPanel.transform.GetChild(0).transform.Find
            (saveManager.dataSheet.relicDataSheet[relicsInShop[vilShopNode.curNumber]].relicName + "(Clone)");
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
            // RelicManager.Instance.GetRelic(relicsInShop[vilShopNode.curNumber]);
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

    public void PanelOpenBtn()
    {
        ShopPanel.SetActive(true);
    }

    public void PanelCloseBtn()
    {
        for (int i = 0; i < 6; i++)
            isSoldOut[i] = false;
        ShopPanel.SetActive(false);
    }
}
