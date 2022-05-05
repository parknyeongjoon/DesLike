using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] TMP_Text[] SelText = new TMP_Text[6]; // 각 선택지별 텍스트 - 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 

    int curDay = 0, curStage; // 현재 날짜
    bool midBossCheck1, midBossCheck2, villageCheck, organCheck, isContinue; // 중간 보스, 마을, 정비, 이어하기 여부
    int[] nxtEvnt = new int[3];
    int nodeNum;   // 노드 지정용(nxtEvnt[nodeNum]용)
    int curTrack = 0;
    bool newSet;

    [SerializeField] GameObject MyTeamPanel, MorePanel;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject[] TrackNode;    // 외길, 두갈래길, 세갈래길
    [SerializeField] GameObject[] Button1_1;    // 0 : 1차 중간 / 1 : 2차 중간 / 2 : 최종 / 3 : 마을 / 4 : 정비
    [SerializeField] GameObject[] Button2_1;    // 6개
    [SerializeField] GameObject[] Button2_2;    // 6개
    [SerializeField] GameObject[] Button3_1;    // 6개
    [SerializeField] GameObject[] Button3_2;    // 6개
    [SerializeField] GameObject[] Button3_3;    // 6개
    [SerializeField] GameObject[] EventButton;  // 0~4 : 2_1 ~ 3_3
    [SerializeField] GameObject[] Title;    // 0 전투 / 1 이벤트

    [SerializeField] BattleNodeScript[] T1B1; // Stage1, Track1, Button1
    [SerializeField] BattleNodeScript[] T2B1; // Stage1, Track2, Button1
    [SerializeField] BattleNodeScript[] T2B2; // Stage1, Track2, Button2
    [SerializeField] BattleNodeScript[] T3B1; // Stage1, Track3, Button1
    [SerializeField] BattleNodeScript[] T3B2; // Stage1, Track3, Button2
    [SerializeField] BattleNodeScript[] T3B3; // Stage1, Track3, Button3
    [SerializeField] GameObject[] EventNode = new GameObject[7]; // 0~4 : 2_1 ~ 3_3; 5 : 마을, 6 : 정비
    CurBattle curBattle;
    int[] nextEvent;   // [0~2] : Btn1~3, int : 0~2 => 랜덤값 배정(전투 배정)
    bool[] isChallenge = new bool[3];

    public Map map;
    SaveManager saveManager;

    [SerializeField] PortDatas portDatas;
    int selectNum = 0;

    void Start()
    {
        saveManager = SaveManager.Instance;
        saveManager.gameData.mapData.curWindow = CurWindow.Map;

        FindData(); // 데이터 찾기
        ObjectInactive();   // 맵 초기화
        TrackSetting(); // 트랙 세팅(갈랫길, 버튼, 세부 이벤트 등)
        saveManager.gameData.mapData.newSet = false;
        saveManager.SaveGameData(); // 저장
    }

    void FindData() // 외부 데이터 가져오기
    {
        newSet = saveManager.gameData.mapData.newSet;
        curDay = saveManager.gameData.mapData.curDay;
        curStage = saveManager.gameData.mapData.curStage;
        midBossCheck1 = saveManager.gameData.mapData.midBossCheck1;
        midBossCheck2 = saveManager.gameData.mapData.midBossCheck2;
        villageCheck = saveManager.gameData.mapData.villageCheck;
        organCheck = saveManager.gameData.mapData.organCheck;
        curBattle = saveManager.gameData.mapData.curBattle;
    }

    void ObjectInactive()   // 맵 초기화
    {
        InfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                Track[i].SetActive(false);
                TrackNode[i].SetActive(false);  // 길 노드
                T1B1[i].gameObject.SetActive(false);
                map.selectNode[i] = null;
            }
      
            T2B1[i].gameObject.SetActive(false);
            T2B2[i].gameObject.SetActive(false);
            T3B1[i].gameObject.SetActive(false);
            T3B2[i].gameObject.SetActive(false);
            T3B3[i].gameObject.SetActive(false);
        }
    }

    void TrackSetting()    // 트랙 세팅
    {
        Debug.Log("Track Setting");
        if (newSet == true) // 처음 세팅
        {
            if (curDay >= 10 && midBossCheck1 == false)    // 1차 중간 보스 라운드
            {
                nxtEvnt[0] = 0;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.midBossCheck1 = true;
            }
            else if (curDay >= 15 && villageCheck == false) // 마을 라운드 
            {
                nxtEvnt[0] = 3;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.villageCheck = true;
            }
            else if (curDay >= 20 && midBossCheck2 == false) // 2차 중간 보스 라운드
            {
                nxtEvnt[0] = 1;
                curTrack = 0;
                saveManager.gameData.mapData.curTrack = 0;
                saveManager.gameData.mapData.midBossCheck2 = true;

            }
            else if (curDay >= 29 && villageCheck == true)  // 정비 라운드
            {
                if (organCheck == false)
                {
                    nxtEvnt[0] = 4;
                    curTrack = 0;
                    saveManager.gameData.mapData.curTrack = 0;
                    saveManager.gameData.mapData.organCheck = true;
                }
                else
                {
                    nxtEvnt[0] = 2;    // 최종 보스
                    curTrack = 0;
                    saveManager.gameData.mapData.curTrack = 0;
                }
            }
            else
            {
                curTrack = Random.Range(0, 2) + 1; // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정
                saveManager.gameData.mapData.curTrack = curTrack;   // 저장
            }
        }
        else curTrack = saveManager.gameData.mapData.curTrack;  // 기존 세팅 데이터 가져오기

        Track[curTrack].SetActive(true);
        Debug.Log(curTrack);
        Debug.Log("Track SetActive");

        NextEventChoice();

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
        }
        Debug.Log("Track Setting 완료");
    }

    void NextEventChoice()  // 0 : 전투, 1 : 이벤트, 2 : 중간 보스, 3 : 마을, 4 : 정비, 5 : 최종 보스
    {
        if (newSet == true && curTrack != 0)
        {
            int cRandom;
            for (int i = 0; i < 3; i++) // 두,세갈래길 랜덤(전투, 이벤트) 설정
            {
                if (curDay == 0)
                    nxtEvnt[i] = 0;
                else nxtEvnt[i] = Random.Range(0, 2);    // 전투(0) or 이벤트(1)
                saveManager.gameData.mapData.selEvent[i] = nxtEvnt[i];  // 데이터 저장

                if (nxtEvnt[i] == 1) // 이벤트라면
                    saveManager.gameData.mapData.evntList[i] = Random.Range(0, 6);  // 이벤트 리스트에 따라서 다름; 세부 확률 조정 필요
                else saveManager.gameData.mapData.evntList[i] = 0;  // 전투면 0 표시

                // 도전모드 관련 코드
                isChallenge[i] = false; // 초기화
                cRandom = Random.Range(0, 5);   // 20퍼 확률
                if (cRandom == 4 && nxtEvnt[i] == 0)   // 도전모드 실행
                {
                    isChallenge[i] = true;
                    saveManager.gameData.mapData.isChallenge[i] = isChallenge[i];   // 데이터 저장
                    Debug.Log(i + 1 + "도전모드 활성화");
                }
            }
        }
        else if (curTrack != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                nxtEvnt[i] = saveManager.gameData.mapData.selEvent[i];
                isChallenge[i] = saveManager.gameData.mapData.isChallenge[i];
            }
        }
    }

    void OneTrackSet()  // 외길 세팅
    {
        TrackNode[0].SetActive(true);

        switch (nxtEvnt[0])
        {
            case 0: // 1차 중간 보스
                SelText[0].text = "중간 보스";
                curBattle = CurBattle.MidBoss1;
                BattleNodeSet(0);
                break;
            case 1: // 2차 중간 보스
                SelText[0].text = "중간 보스";
                curBattle = CurBattle.MidBoss2;
                BattleNodeSet(0);
                break;

            case 2: // 최종 보스
                SelText[0].text = "최종 보스";
                curBattle = CurBattle.StageBoss;
                BattleNodeSet(0);
                break;

            case 3: // 마을
                SelText[0].text = "마을";
                EventNodeSet(0);
                break;

            case 4: // 정비
                SelText[0].text = "정비";
                EventNodeSet(0);
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
        TrackNode[1].SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            if (curDay == 0 || nxtEvnt[i] == 0)
            {
                SelText[i + 1].text = "전투";
                curBattle = CurBattle.Normal;
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
            if (curDay == 0 || nxtEvnt[i] == 0)
            {
                SelText[i + 3].text = "전투";
                curBattle = CurBattle.Normal;
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
        if (curTrack == 1) // 2트랙
        {
            if (btn == 0)
            {
                if (isChallenge[0] == true) T2B1[(curDay / 5) + 1].gameObject.SetActive(true);
                else T2B1[curDay / 5].gameObject.SetActive(true); // 버튼1
            }
            else
            {
                if (isChallenge[1] == true) T2B2[(curDay / 5) + 1].gameObject.SetActive(true);
                else T2B2[curDay / 5].gameObject.SetActive(true); // 버튼2
            }
        }
        else if (curTrack == 2)   // 3트랙
        {
            if (btn == 0)
            {
                if (isChallenge[0] == true) T3B1[(curDay / 5) + 1].gameObject.SetActive(true);
                else T3B1[curDay / 5].gameObject.SetActive(true); // 버튼1
            }
            else if (btn == 1)
            {
                if (isChallenge[1] == true) T3B2[(curDay / 5) + 1].gameObject.SetActive(true);
                else T3B2[curDay / 5].gameObject.SetActive(true); // 버튼2
            }
            else
            {
                if (isChallenge[2] == true) T3B3[(curDay / 5) + 1].gameObject.SetActive(true);
                else T3B3[curDay / 5].gameObject.SetActive(true); // 버튼3
            }
        }
        else // 1트랙
            T1B1[nxtEvnt[0]].gameObject.SetActive(true);    // 0 : 1차 중간 / 1 : 2차 중간 / 2 : 최종
    }

    void EventNodeSet(int btn)
    {
        if(curTrack == 0) // 1트랙
        {
            if (nxtEvnt[0] == 3) EventNode[5].gameObject.SetActive(true);
            else EventNode[6].gameObject.SetActive(true);
        }
        else if (curTrack == 1) // 2트랙
        {
            if (btn == 0) EventNode[0].gameObject.SetActive(true);  // 버튼1
            else EventNode[1].gameObject.SetActive(true); // 버튼2
        }
        else
        {
            if (btn == 0) EventNode[2].gameObject.SetActive(true);
            else if (btn == 1) EventNode[3].gameObject.SetActive(true);
            else EventNode[4].gameObject.SetActive(true);
        }    
    }

    
    // 버튼용 함수들

    public void Button1()
    {
        selectNum = 1;
        saveManager.gameData.mapData.curBtn = 0;
        InfoPanel.SetActive(true);
        if (curTrack == 0) // 1트랙
        {
            switch (nxtEvnt[0])
            {
                case 0: // 1차 중간 보스
                    Button1_1[0].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;
                    
                case 1: // 2차 중간 보스
                    Button1_1[1].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 2: // 최종 보스
                    Button1_1[2].SetActive(true);
                    Title[0].gameObject.SetActive(true);
                    break;

                case 3: // 마을
                    Button1_1[3].SetActive(true);
                    break;

                case 4: // 정비
                    Button1_1[4].SetActive(true);
                    break;

            }
        }

        else if (curTrack == 1) // 2트랙
        {
            if (curDay == 0 || nxtEvnt[0] == 0)
            {
                if (isChallenge[0] == true) Button2_1[(curDay / 5) + 1].SetActive(true);
                else Button2_1[curDay / 5].SetActive(true);
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
            if (curDay == 0 || nxtEvnt[0] == 0)
            {
                if (isChallenge[0] == true) Button3_1[curDay / 5 + 1].SetActive(true);
                else Button3_1[curDay / 5].SetActive(true);
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
        saveManager.gameData.mapData.curBtn = 1;
        InfoPanel.SetActive(true);
        if (curTrack == 1) // 2_2
        {
            if (curDay == 0 || nxtEvnt[1] == 0)
            {
                if (isChallenge[1] == true) Button2_2[curDay / 5 + 1].SetActive(true);
                else Button2_2[curDay / 5].SetActive(true);
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
            if (curDay == 0 || nxtEvnt[1] == 0)
            {
                if (isChallenge[1] == true) Button3_2[curDay / 5 + 3].SetActive(true);
                else Button3_2[curDay / 5].SetActive(true);
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
        saveManager.gameData.mapData.curBtn = 2;
        InfoPanel.SetActive(true);
        if (curDay == 0 || nxtEvnt[2] == 0)
        {
            if (isChallenge[2] == true) Button3_3[curDay / 5 + 3].SetActive(true);
            else Button3_3[curDay / 5].SetActive(true);
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
        for(int i = 0; i<6; i++)
        {
            if (i < 2) Title[i].SetActive(false);
            if (i < 5) Button1_1[i].SetActive(false);
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
        switch (curTrack) 
        {
            case 0:
                if (nxtEvnt[0] <= 2)
                {
                    if (isChallenge[0] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;

            case 1: // 2트랙
                if (nxtEvnt[selectNum - 1] == 0)
                {
                    if (isChallenge[1] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;

            case 2: // 3트랙
                if (nxtEvnt[selectNum - 1] == 0)
                {
                    if (isChallenge[2] == true) saveManager.gameData.mapData.challengeCount += 1;
                    saveManager.gameData.mapData.curDay += 2;
                }
                break;
        }
        for(int i = 0; i<3; i++)
            saveManager.gameData.mapData.isAbleSet[i] = false;
        saveManager.gameData.mapData.curBattle = curBattle;
        saveManager.gameData.mapData.newSet = true; // 맵 탈출
        InfoPanelClose();
    }
}