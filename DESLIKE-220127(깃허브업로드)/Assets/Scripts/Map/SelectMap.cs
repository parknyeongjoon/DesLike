using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] Text HPText, CurrentDayText, MoneyText;
    [SerializeField] Text[] SelText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0; // 현재 날짜
    bool midBossCheck = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부
    int[] selDay = new int[3];
    int[] selEvnt = new int[6]; // 0 : 1_1 / 1, 2 : 2_1, 2 / 3, 4, 5 : 3_1, 2, 3
    int nodeNum;   // 노드 지정용(selEvnt[nodeNum]용)
    int curTrack = 0;

    [SerializeField] GameObject MyTeamPanel, MorePanel;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] StartBtn = new GameObject[14];
    [SerializeField] GameObject[] Nodes = new GameObject[14];
    /*
    - 0~3 => 외길 { 0 : 중간 보스; 1 : 마을; 2 : 정비; 3 : 최종 보스 }
    - 4~7 => 두갈래길 { 4,5 : 2_1, 2 전투; 6, 7 : 2_1, 2 이벤트 }
    - 8~13 => 세갈래길 { 8, 9, 10 : 3_1, 2, 3 전투; 11, 12, 12 : 3_1, 2, 3 이벤트 }
    */
    

    public Map map;
    SaveManager saveManager;

    [SerializeField]
    PortDatas portDatas;
    GoodsCollection goodsCollection;

    void Start()
    {
        saveManager = SaveManager.Instance;
        ObjectInactive();   // 맵 초기화
        FindData(); // 데이터 찾기
        CommonSetting(); // 일반 세팅
      
        if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // 일반적인 상황(이벤트 or 전투)이 아니라면
            curTrack = 0;   // 외길
        else curTrack = Random.Range(0, 2) + 1; // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

        switch (curTrack)
        {
            case 0: // 외길
                OneTrackSet();
                break;
            case 1: // 두갈래길
                TwoTrackSet();
                break;
            case 2: // 세갈래길
                ThreeTrackSet();
                break;
            default:    // 특수 상황 외길
                Debug.Log("오류 발생");
                break;
        }
        Track[curTrack].SetActive(true);
    }

    void ObjectInactive()   // 맵 초기화
    {
        InfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < 14; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // 길 노드
            Nodes[i].SetActive(false);
        }
    }

    void FindData() // 외부 데이터 가져오기
    {
        curDay = saveManager.gameData.map.curDay;
        midBossCheck = saveManager.gameData.map.midBossCheck;
        villageCheck = saveManager.gameData.map.villageCheck;
        organCheck = saveManager.gameData.map.organCheck;
        goodsCollection = saveManager.gameData.goodsCollection;
    }
    
    void CommonSetting()    // 공통 세팅
    {
        // hero 체력 가져오기
        NextEventChoice(); // 다음 내용 조정
        CurrentDayText.text = curDay + " / 30";
        if (curDay == 0)
            MoneyText.text = "- 식량 : 0 \n- 골드 : 0";
        else
            MoneyText.text = "- 식량 : " + goodsCollection.food + "\n- 골드 : " + goodsCollection.gold;
    }
    
    void NextEventChoice()  // 0 : 전투, 1 : 이벤트, 2 : 중간 보스, 3 : 마을, 4 : 정비, 5 : 최종 보스
    {
        for (int i = 1; i < 6; i++) // 두,세갈래길 랜덤(전투, 이벤트) 설정
            selEvnt[i] = Random.Range(0, 2);
        
        if (curDay >= 14)
        {
            if (midBossCheck == false)    // 중간 보스 라운드
            {
                selEvnt[0] = 2;
                saveManager.gameData.map.midBossCheck = true;
            }
            else if (villageCheck == false)
            {
                selEvnt[0] = 3;
                saveManager.gameData.map.villageCheck = true;
            }
            else if (curDay >= 29 && villageCheck == true)  // 정비 라운드
            {
                if (organCheck == false)
                {
                    selEvnt[0] = 4;
                    saveManager.gameData.map.organCheck = true;
                }
                else selEvnt[0] = 5;
            }
        }
    }

   
    void OneTrackSet()  // 외길 세팅
    {
        TrackNode[0].SetActive(true);

        switch (selEvnt[0] - 2)
        {
            case 0: // 중간 보스
                SelText[0].text = "중간 보스";
                Nodes[0].SetActive(true);
                // 중간 보스 데이터 세팅
                break;
            case 1: // 마을
                SelText[0].text = "마을";
                Nodes[1].SetActive(true);
                // 마을 데이터 세팅
                break;
            case 2: // 정비
                SelText[0].text = "정비";
                Nodes[2].SetActive(true);
                // 정비 데이터 세팅
                break;
            case 3: // 최종 보스
                SelText[0].text = "최종 보스";
                Nodes[3].SetActive(true);
                // 최종 보스 데이터 세팅
                break;
            default:
                SelText[0].text = "에러";
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
        TrackNode[1].SetActive(true);

    for (int i = 0; i < 2; i++)
        {
            if (curDay == 0 || selEvnt[i+1] == 0)
            {
                SelText[i + 1].text = "전투";
                Nodes[i+4].SetActive(true);
                // 전투 데이터 세팅
            }
            else
            {
                SelText[i + 1].text = "이벤트";
                Nodes[i + 6].SetActive(true);
                // 이벤트 데이터 세팅
            }
        }
    }

    void ThreeTrackSet()    // 세갈래길 세팅
    {
        TrackNode[2].SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            int j = i + 3;
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 3].text = "전투";
                Nodes[i + 8].SetActive(true);
                // 전투 데이터 세팅
            }
            else
            {
                SelText[i + 3].text = "이벤트";
                Nodes[i + 11].SetActive(true);
                // 이벤트 데이터 세팅
            }
        }
    }

    /* 
    public void ButtonDown1_1()
    {
        Info[0].SetActive(true);
    }
    
    public void ButtonDown2_1()
    {
        Info[1].SetActive(true);
    }

    public void ButtonDown2_2()
    {
        Info[2].SetActive(true);
    }

    public void ButtonDown3_1()
    {
        Info[3].SetActive(true);
    }

    public void ButtonDown3_2()
    {
        Info[4].SetActive(true);
    }

    public void ButtonDown3_3()
    {
        Info[5].SetActive(true);
    }

    public void ButtonOut()
    {
        for (int i = 0; i < 6; i++)
            Info[i].SetActive(false);
    }
    */

    public void Button1()
    {
        switch(curTrack)
        {
            case 0: // 1_1
                switch(selEvnt[0])
                {
                    case 0:
                        nodeNum = 0;    // 중간 보스
                        break;

                    case 1:
                        nodeNum = 1;    // 마을
                        break;

                    case 2:
                        nodeNum = 2;    // 정비 노드 추가 필요
                        break;

                    case 3:
                        nodeNum = 3;    // 최종 보스
                        break;

                    default:
                        Debug.Log("오류 발생");
                        break;
                }
                break;

            case 1: // 2_1
                if(selEvnt[1] == 0 || curDay == 0)
                    nodeNum = 4;
                else nodeNum = 6;
                break;

            case 2: // 3_1
                if (selEvnt[3] == 0 || curDay == 0)
                    nodeNum = 8;
                else nodeNum = 11;
                break;

            default:
                Debug.Log("오류 발생");
                break;
        }
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }

    public void Button2()
    {
        switch (curTrack)
        {   
            case 1: // 2_2
                if (selEvnt[2] == 0 || curDay == 0)
                    nodeNum = 5;
                else nodeNum = 7;
                break;
                
            case 2: // 3_2
                if (selEvnt[4] == 0 || curDay == 0)
                    nodeNum = 9;
                else nodeNum = 12;
                break;
        }
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }


    public void Button3()
    {
        if(selEvnt[5] == 0)
            nodeNum = 10;    // 전투
        else nodeNum = 13;    // 이벤트
        Nodes[nodeNum].gameObject.SetActive(true);
        InfoPanel.gameObject.SetActive(true);
        StartBtn[nodeNum].gameObject.SetActive(true);
    }

    public void InfoPanelClose()
    {
        StartBtn[nodeNum].SetActive(false);   // 이벤트 노드
        InfoPanel.gameObject.SetActive(false);
    }

    public void MorePanelClose()
    {
        MorePanel.SetActive(false);
    }

    public void MyTeamPanelOpen()
    {
        MyTeamPanel.gameObject.SetActive(true);
    }
    
    public void MyTeamPanelClose()
    {
        MyTeamPanel.gameObject.SetActive(false);
    }
    
    public void GameStart()
    {
        map.level = curDay;
        if (nodeNum >= 0 && nodeNum <=3)
            saveManager.gameData.map.curDay += 1;  // 특수 이벤트 1일 추가
        else if (nodeNum <= 10)
            if(nodeNum !=6 && nodeNum !=7)
                saveManager.gameData.map.curDay += 2;  // 전투 이벤트 2일 추가
        InfoPanelClose();
    }
}