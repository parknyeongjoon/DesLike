using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] TMP_Text HPText, CurrentDayText, MoneyText;
    [SerializeField] TMP_Text[] SelText = new TMP_Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0, curStage; // 현재 날짜
    bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부
    int[] selDay = new int[3];
    int[] selEvnt = new int[3];
    int nodeNum;   // 노드 지정용(selEvnt[nodeNum]용)
    int curTrack = 0;
    bool newSet = false;

    [SerializeField] GameObject MyTeamPanel, MorePanel;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] Button1_1;    // 0-2 1차 중간 / 3-5 2차 중간 / 6-8 최종 / 9 마을 / 10 정비
    [SerializeField] GameObject[] Button2_1;
    [SerializeField] GameObject[] Button2_2;
    [SerializeField] GameObject[] Button3_1;
    [SerializeField] GameObject[] Button3_2;
    [SerializeField] GameObject[] Button3_3;
    [SerializeField] GameObject[] EventButton;
    [SerializeField] GameObject[] Title;    // 0 전투 / 1 이벤트

    [SerializeField] BattleNodeScript[] S1T1B1; // Stage1, Track1, Button1
    [SerializeField] BattleNodeScript[] S1T2B1; // Stage1, Track2, Button1
    [SerializeField] BattleNodeScript[] S1T2B2; // Stage1, Track2, Button2
    [SerializeField] BattleNodeScript[] S1T3B1; // Stage1, Track3, Button1
    [SerializeField] BattleNodeScript[] S1T3B2; // Stage1, Track3, Button2
    [SerializeField] BattleNodeScript[] S1T3B3; // Stage1, Track3, Button3
    [SerializeField] GameObject[] EventNode = new GameObject[7]; // 0:마을 / 1:정비 / 2,3 : 2_1,2이벤트 / 4,5,6 : 3_1,2,3 이벤트
    int[] nextEvent;   // [0~2] : Btn1~3, int : 0~2 => 랜덤값 배정(전투 배정)
    
    public Map map;
    SaveManager saveManager;

    [SerializeField] PortDatas portDatas;
    GoodsCollection goodsCollection;
    int selectNum = 0;

    void Start()
    {
        
        saveManager = SaveManager.Instance;
        ObjectInactive();   // 맵 초기화
        FindData(); // 데이터 찾기
        CommonSetting(); // 일반 세팅
        NextEventChoice(); // 다음 내용 조정

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

        Track[curTrack].SetActive(true);

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
    }

    void ObjectInactive()   // 맵 초기화
    {
        InfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < 18; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // 길 노드
            if (i < 9) S1T1B1[i].gameObject.SetActive(false);
            S1T2B1[i].gameObject.SetActive(false);
            S1T2B2[i].gameObject.SetActive(false);
            S1T3B1[i].gameObject.SetActive(false);
            S1T3B2[i].gameObject.SetActive(false);
            S1T3B3[i].gameObject.SetActive(false);
        }
    }

    void FindData() // 외부 데이터 가져오기
    {
        curDay = saveManager.gameData.map.curDay;

        if (curDay != saveManager.gameData.map.checkDay || curDay == 0)    // 이번 라운드 맵에 처음 온다면
        {
            newSet = true;
            saveManager.gameData.map.checkDay = curDay;
        }
        curStage = saveManager.gameData.map.curStage;
        curTrack = saveManager.gameData.map.curTrack;
        nextEvent = saveManager.gameData.map.nextEvent;
        midBossCheck1 = saveManager.gameData.map.midBossCheck1;
        midBossCheck2 = saveManager.gameData.map.midBossCheck2;
        villageCheck = saveManager.gameData.map.villageCheck;
        organCheck = saveManager.gameData.map.organCheck;
        goodsCollection = saveManager.gameData.goodsCollection;
        for(int i = 0; i<3; i++) selEvnt[i] = saveManager.gameData.map.selEvent[i];
    }

    void CommonSetting()    // 공통 세팅
    {
        // hero 체력 가져오기
        CurrentDayText.text = curDay + " / 30";
        if (curDay == 0)
            MoneyText.text = "- 골드 : 0";
        else
            MoneyText.text = "- 골드 : " + goodsCollection.gold;

        if (newSet == true)
        {
            if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // 일반적인 상황(이벤트 or 전투)이 아니라면
                curTrack = 0;   // 외길
            else curTrack = Random.Range(0, 2) + 1; // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정
        }
    }

    void NextEventChoice()  // 0 : 전투, 1 : 이벤트, 2 : 중간 보스, 3 : 마을, 4 : 정비, 5 : 최종 보스
    {
        if (newSet == true)
        {
            for (int i = 0; i < 3; i++) // 두,세갈래길 랜덤(전투, 이벤트) 설정
            {
                selEvnt[i] = Random.Range(0, 2);
                saveManager.gameData.map.selEvent[i] = selEvnt[i];  // 데이터 저장
            }

            // 특수 상황
            if (curDay >= 10 && midBossCheck1 == false)    // 1차 중간 보스 라운드
            {
                selEvnt[0] = 2;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.midBossCheck1 = true;
            }
            else if (curDay >= 15 && villageCheck == false) // 마을 라운드 
            {
                selEvnt[0] = 3;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.villageCheck = true;
            }
            else if (curDay >= 20 && midBossCheck2 == false) // 2차 중간 보스 라운드
            {
                selEvnt[0] = 4;
                curTrack = 0;
                saveManager.gameData.map.curTrack = 0;
                saveManager.gameData.map.midBossCheck2 = true;

            }
            else if (curDay >= 29 && villageCheck == true)  // 정비 라운드
            {
                if (organCheck == false)
                {
                    selEvnt[0] = 5;
                    curTrack = 0;
                    saveManager.gameData.map.curTrack = 0;
                    saveManager.gameData.map.organCheck = true;
                }
                else selEvnt[0] = 6;    // 최종 보스
            }

            for (int i = 0; i < 3; i++)
            {
                nextEvent[i] = Random.Range(0, 3);  // 랜덤값 지정
                saveManager.gameData.map.nextEvent[i] = nextEvent[i];   // 데이터 저장
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
                BattleNodeSet(0);
                break;
            case 1: // 마을
                SelText[0].text = "마을";
                EventNodeSet(0);
                break;
            case 2:
                SelText[0].text = "중간 보스";
                BattleNodeSet(0);
                break;

            case 3: // 정비
                SelText[0].text = "정비";
                EventNodeSet(0);
                break;

            case 4: // 최종 보스
                SelText[0].text = "최종 보스";
                BattleNodeSet(0);
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
        TrackNode[1].SetActive(true);
        
        for (int i = 0; i < 2; i++)
        {
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 1].text = "전투";
                BattleNodeSet(i);
            }
            else
            {
                SelText[i + 1].text = "이벤트";
                EventNodeSet(i);
            }
        }
    }

    void ThreeTrackSet()    // 세갈래길 세팅
    {
        TrackNode[2].SetActive(true);
        
        for (int i = 0; i < 3; i++)
        {
            if (curDay == 0 || selEvnt[i] == 0)
            {
                SelText[i + 3].text = "전투";
                BattleNodeSet(i);
            }
            else
            {
                SelText[i + 3].text = "이벤트";
                EventNodeSet(i);
            }
        }
    }

    void BattleNodeSet(int btn)
    {
        switch (curStage)
        {
            case 0: // 스테이지1
                if (curTrack == 1) // 2트랙
                {
                    if (btn == 0) S1T2B1[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true); // 버튼1
                    else S1T2B2[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);  // 버튼2
                }
                else if (curTrack == 2)   // 3트랙
                {
                    if (btn == 0) S1T3B1[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true); // 버튼1
                    else if (btn == 1) S1T3B2[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);    // 버튼2
                    else S1T3B3[nextEvent[btn] + 3 * (curDay / 5)].gameObject.SetActive(true);  // 버튼3
                }
                else // 1트랙
                    S1T1B1[nextEvent[btn] + 3 * (selEvnt[0] / 2 - 1)].gameObject.SetActive(true);    // 0~2 : 1차 중간 / 3~5 : 2차 중간 / 6~8 : 3차 중간
                break;

            case 1: // 2스테이지
                break;

            case 2: // 3스테이지
                break;
        }
    }

    void EventNodeSet(int btn)
    {
        switch (curStage)
        {
            case 0: // 스테이지1
                if (curTrack == 1) // 2트랙
                {
                    if (btn == 0) EventNode[2].gameObject.SetActive(true);  // 버튼1
                    else EventNode[3].gameObject.SetActive(true); // 버튼2
                }
                else if (curTrack == 2)   // 3트랙
                {
                    if (btn == 0) EventNode[4].gameObject.SetActive(true);
                    else if (btn == 1) EventNode[5].gameObject.SetActive(true);
                    else EventNode[6].gameObject.SetActive(true);
                }
                else // 1트랙
                {
                    if (selEvnt[0] == 3) EventNode[0].gameObject.SetActive(true);
                    else EventNode[1].gameObject.SetActive(true);
                }
                break;

            case 1: // 2스테이지
                break;

            case 2: // 3스테이지
                break;
        }
    }

    public void Button1()
    {
        selectNum = 1;
        InfoPanel.SetActive(true);
        if (curTrack == 0) // 1트랙
        {
            switch (selEvnt[0] - 2)
            {
                case 0: // 1차 중간 보스
                    Button1_1[nextEvent[0]].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 1: // 마을
                    Button1_1[9].SetActive(true);
                    break;

                case 2: // 2차 중간 보스
                    Button1_1[nextEvent[0] + 3].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 3: // 정비
                    Button1_1[10].SetActive(true);
                    break;

                case 4: // 최종 보스
                    Button1_1[nextEvent[0] + 6].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;
            }
        }

        else if (curTrack == 1) // 2트랙
        {
            if (curDay == 0 || selEvnt[0] == 0)
            {
                Button2_1[nextEvent[0] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[0].SetActive(true);
                Title[1].SetActive(true);
            }
        }
        else    // 3트랙
        {
            if (curDay == 0 || selEvnt[0] == 0)
            {
                Button3_1[nextEvent[0] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[2].SetActive(true);
                Title[1].SetActive(true);
            }
        }
    }

    public void Button2()
    {
        selectNum = 2;
        InfoPanel.SetActive(true);
        if (curTrack == 1) // 2_2
        {
            if (curDay == 0 || selEvnt[1] == 0)
            {
                Button2_2[nextEvent[1] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[1].SetActive(true);
                Title[1].SetActive(true);
            }
        }
        else    // 3_2
        {
            if (curDay == 0 || selEvnt[1] == 0)
            {
                Button3_2[nextEvent[1] + 3 * (curDay / 5)].SetActive(true);
                Title[0].SetActive(true);
            }
            else
            {
                EventButton[3].SetActive(true);
                Title[1].SetActive(true);
            }
        }
    }

    public void Button3()
    {
        selectNum = 3;            
        InfoPanel.SetActive(true);
        if (curDay == 0 || selEvnt[2] == 0)
        {
            Button3_3[nextEvent[2] + 3 * (curDay / 5)].SetActive(true);
            Title[0].SetActive(true);
        }
        else
        {
            EventButton[4].SetActive(true);
            Title[1].SetActive(true);
        }
    }

    public void InfoPanelClose()
    {
        
        for(int i = 0; i<18; i++)
        {
            if (i < 2) Title[i].SetActive(false);
            if (i < 11) Button1_1[i].SetActive(false);
            Button2_1[i].SetActive(false);
            Button2_2[i].SetActive(false);
            Button3_1[i].SetActive(false);
            Button3_2[i].SetActive(false);
            Button3_3[i].SetActive(false);
        }
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
        switch(curTrack)    // curDay 추가
        {
            case 0:
                if (selEvnt[0] % 2 == 0)
                    saveManager.gameData.map.curDay += 2;
                break;

            case 1: // 2트랙
                if (curDay == 0 || selEvnt[selectNum-1] == 0)
                    saveManager.gameData.map.curDay += 2;
                break;

            case 2: // 3트랙
                if (curDay == 0 || selEvnt[selectNum-1] == 0)
                    saveManager.gameData.map.curDay += 2;
                break;
        }
        InfoPanelClose();
    }
}