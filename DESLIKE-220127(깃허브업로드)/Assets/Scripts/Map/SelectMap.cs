using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Track = new GameObject[3];
    [SerializeField] Text HPText, CurrentDayText, MoneyText;
    [SerializeField] Text[] SelText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 
    [SerializeField] Text[] InfoText = new Text[6]; // 0 : 1_1 / 1,2 : 2_1, 2 / 3, 4, 5 = 3_1, 2, 3 

    GameObject hero;
    HeroInfo heroInfo;

    int curDay = 0; // 현재 날짜
    bool midBossCheck = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부
    int[] selDay = new int[3];
    int[] selEvnt = new int[3];

    [SerializeField] GameObject[] TrackNode = new GameObject[3];
    [SerializeField] GameObject[] BattleNode = new GameObject[5]; // 0, 1 : 2_1, 2 / 2, 3, 4 = 3_1, 2, 3 
    [SerializeField] GameObject[] EvntNode = new GameObject[5];// 0, 1 : 2_1, 2 / 2, 3, 4 = 3_1, 2, 3 
    [SerializeField] GameObject[] ExtraNode = new GameObject[4]; // 0 : 중간 보스, 1 : 마을, 2 : 정비, 3 : 최종 보스
    [SerializeField] GameObject[] Info = new GameObject[6]; // 0 : 1_1 / 1, 2 : 2_1, 2 / 3, 4, 5 : 3_1, 2, 3


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
        
        int mapSelect = 0; // mapSelect 선언 및 초기화용
       
        if ((selEvnt[0] != 0) && (selEvnt[0] != 1)) // 일반적인 상황(이벤트 or 전투)이 아니라면
            mapSelect = 3;   // 외길
        else mapSelect = Random.Range(0, 2); // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정

        for (int i = 0; i < 3; i++)
            Track[i].SetActive(false);

        switch(mapSelect)
        {
            case 0: // 두갈래길
                Track[1].SetActive(true);
                TwoTrackSet();
                break;
            case 1: // 세갈래길
                Track[2].SetActive(true);
                ThreeTrackSet();
                break;
            default:    // 특수 상황 외길
                Track[0].SetActive(true);
                OneTrackSet();
                break;
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
        if (curDay != 0)
        {
            hero = GameObject.Find("Hero");
            heroInfo = hero.GetComponent<HeroInfo>();
            HPText.text = heroInfo.cur_Hp + "/" + heroInfo.castleData.hp;
        }
        NextEventChoice(); // 다음 내용 조정
        CurrentDayText.text = curDay + " / 30";
        MoneyText.text = "- 식량 : " + goodsCollection.food + "\n- 골드 : " + goodsCollection.gold;
    }

    void ObjectInactive()   // 맵 초기화
    {
        for(int i = 0; i<5; i++)
        {
            if (i < 3) TrackNode[i].SetActive(false);  // 길 노드
            if (i < 4) ExtraNode[i].SetActive(false);  // 기타 노드
            BattleNode[i].SetActive(false); // 배틀 노드
            EvntNode[i].SetActive(false);   // 이벤트 노드
        }
    }

    void NextEventChoice()  // 0 : 전투, 1 : 이벤트, 2 : 중간 보스, 3 : 마을, 4 : 정비, 5 : 최종 보스
    {
        // 기본 랜덤 설정
        for (int i = 0; i < 3; i++)
            selEvnt[i] = Random.Range(0, 2);

        if (curDay == 0) // 첫 라운드는 무조건 전투
            for (int i = 0; i < 3; i++)
                selEvnt[i] = 0;
        else
        {
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
    }

    void OneTrackSet()  // 외길 세팅
    {
        TrackNode[0].SetActive(true);

        for (int i = 0; i < 4; i++)
            ExtraNode[i].SetActive(false);

        selDay[0] = 1;   // 날짜 1로 고정
        switch (selEvnt[0]-2)
        {
            case 0: // 중간 보스
                ExtraNode[0].SetActive(true);
                SelText[0].text = "중간 보스";
                InfoText[0].text = "중간보스 설명";
                break;
            case 1: // 마을
                ExtraNode[1].SetActive(true);
                SelText[0].text = "마을";
                InfoText[0].text = "마을 설명";
                break;
            case 2: // 정비
                    // 노드 추가 필요
                ExtraNode[2].SetActive(true);
                SelText[0].text = "정비";
                InfoText[0].text = "정비 설명";
                break;
            case 3: // 최종 보스
                ExtraNode[3].SetActive(true);
                SelText[0].text = "최종 보스";
                InfoText[0].text = "최종 보스 설명";
                break;
            default:
                SelText[0].text = "에러";
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
        TrackNode[1].SetActive(true);
            
        if (curDay == 0) // 첫 전투
        {
            for(int i = 0; i<2; i++)
            {
                selDay[i] = 1;
                BattleNode[i].SetActive(true);
                SelText[i+1].text = "전투";
                InfoText[i + 1].text = "전투 내용 설명";
            }
        }
        else // 이외
        {
            for(int i = 0; i<2; i++)
            {
                if (selEvnt[i] == 0)
                {
                    BattleNode[i].SetActive(true);
                    SelText[i+1].text = "전투";
                    InfoText[i + 1].text = "전투 내용 설명";
                }
                else
                {
                    EvntNode[i].SetActive(true);
                    SelText[i+1].text = "이벤트";
                    InfoText[i + 1].text = "이벤트 내용 설명";
                }
            }
        }
    }

    void ThreeTrackSet()    // 세갈래길 세팅
    {
            TrackNode[2].SetActive(true);

        if (curDay == 0) // 첫 전투
        {
            for(int i = 0; i<3; i++)
            {
                BattleNode[i+2].SetActive(true);
                SelText[i+3].text = "전투";
                InfoText[i + 3].text = "전투 내용 설명";
            }
        }
        else // 이외
        {
            for(int i = 0; i<3; i++)
            {
                int j = i + 3;
                if (selEvnt[i] == 0)
                {
                    BattleNode[i+2].SetActive(true);
                    SelText[i+3].text = "전투";
                    InfoText[i+3].text = "전투 내용 설명";
                }
                else
                {
                    EvntNode[i+2].SetActive(true);
                    SelText[i+3].text = "이벤트";
                    InfoText[i+3].text = "이벤트 내용 설명";
                }
            }
        }
    }

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

    public void Select1Btn()   // 1번 선택지
    {
        map.level = curDay;
        if (selEvnt[0] == 0)
            saveManager.gameData.map.curDay += 2;  // 전투 시에만 2일 추가
        // 다음 단계 이동 => by 노드
    }

    public void Select2Btn()   // 2번 선택지
    {
        map.level = curDay;
        if (selEvnt[1] == 0)
            saveManager.gameData.map.curDay += 2;  // 전투 시에만 2일 추가
        // 다음 단계 이동 => by 노드
    }

    public void Select3Btn()   // 3번 선택지
    {
        map.level = curDay;
        if (selEvnt[2] == 0)
            saveManager.gameData.map.curDay += 2;  // 전투 시에만 2일 추가
        // 다음 단계 이동 => by 노드
    }
}