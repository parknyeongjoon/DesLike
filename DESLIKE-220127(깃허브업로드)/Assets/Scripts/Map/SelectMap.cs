using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject oneTrack, twoTrack, threeTrack;
    [SerializeField]
    Text HPText, Sel1_1Text, Sel2_1Text, Sel2_2Text, Sel3_1Text, Sel3_2Text, Sel3_3Text, CurrentDayText;

    GameObject hero;
    HeroInfo heroInfo;
    int curDay = 0; // 현재 날짜
    int selDay1, selDay2, selDay3;  // 소모 날짜
    int selEvnt1, selEvnt2, selEvnt3;   // 다음 단계 결정
    
    bool midBossCheck = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부

    [SerializeField]
    GameObject OneTrackNode, TwoTrackNode, ThreeTrackNode;
    [SerializeField]
    GameObject BossNode, VillageNode, EliteNode, BattleNode2_1, BattleNode2_2, BattleNode3_1, BattleNode3_2, BattleNode3_3, 
        EventNode2_1, EventNode2_2, EventNode3_1, EventNode3_2, EventNode3_3;   // 정비 노드는?

    public Map map;
    
    SaveManager saveManager = SaveManager.Instance; // SaveManager 전역 사용

    void Start()
    {
        FindData();
        CommonSetting();
     
        int mapSelect = 0; // mapSelect 선언 및 초기화용

        if ((selEvnt1 != 0) && (selEvnt1 != 1)) // 일반적인 상황(이벤트 or 전투)이 아니라면
            mapSelect = 3;   // 외길
        else mapSelect = Random.Range(0, 2); // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정

        if (mapSelect == 0)
        {
            oneTrack.SetActive(false);
            twoTrack.SetActive(true);
            threeTrack.SetActive(false);
            TwoTrackSet();
        }
        else if (mapSelect == 1)
        {
            oneTrack.SetActive(false);
            twoTrack.SetActive(false);
            threeTrack.SetActive(true);
            ThreeTrackSet();
        }
        else    // 외길, 특수 상황
        {
            oneTrack.SetActive(true);
            twoTrack.SetActive(false);
            threeTrack.SetActive(false);
            OneTrackSet();
        }
    }

    void FindData() // 외부 데이터 가져오기
    {
        curDay = saveManager.gameData.heroSaveData.curDay;
        midBossCheck = saveManager.gameData.heroSaveData.midBossCheck;
        villageCheck = saveManager.gameData.heroSaveData.villageCheck;
        organCheck = saveManager.gameData.heroSaveData.organCheck;
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
    }

    void NextEventChoice()  // 0 : 전투, 1 : 이벤트, 2 : 중간 보스, 3 : 마을, 4 : 정비, 5 : 최종 보스
    {
        // 기본 랜덤 설정
        selEvnt1 = Random.Range(0, 2);
        selEvnt2 = Random.Range(0, 2);
        selEvnt3 = Random.Range(0, 2);

        if (curDay == 0) // 첫 라운드는 무조건 전투
        {
            selEvnt1 = 0;
            selEvnt2 = 0;
            selEvnt3 = 0;
        }
        else
        {
            if (curDay >= 14)
            {
                if (midBossCheck == false)    // 중간 보스 라운드
                {
                    Debug.Log("중간보스");
                    selEvnt1 = 2;
                    saveManager.gameData.heroSaveData.midBossCheck = true;
                }
                else if (villageCheck == false)
                {
                    selEvnt1 = 3;
                    saveManager.gameData.heroSaveData.villageCheck = true;
                }
                else if (curDay >= 29 && villageCheck == true)  // 정비 라운드
                {
                    if (organCheck == false)
                    {
                        selEvnt1 = 4;
                        saveManager.gameData.heroSaveData.organCheck = true;
                    }
                    else selEvnt1 = 5;
                }
            }
        }
    }

    void OneTrackSet()  // 외길 세팅
    {
            OneTrackNode.SetActive(true);
            TwoTrackNode.SetActive(false);
            ThreeTrackNode.SetActive(false);
     
        selDay1 = 1;   // 날짜 1로 고정
        switch (selEvnt1-2)
        {
            case 0: // 중간 보스
                BossNode.SetActive(true);
                VillageNode.SetActive(false);
                EliteNode.SetActive(false);
                Sel1_1Text.text = "중간 보스\n1";
                break;
            case 1: // 마을
                BossNode.SetActive(false);
                VillageNode.SetActive(true);
                EliteNode.SetActive(false);
                Sel1_1Text.text = "마을\n1";
                break;
            case 2: // 정비
                // 노드 추가 필요
                Sel1_1Text.text = "정비\n1";
                break;
            case 3: // 최종 보스
                BossNode.SetActive(false);
                VillageNode.SetActive(false);
                EliteNode.SetActive(true);
                Sel1_1Text.text = "최종 보스\n1";
                break;
            default:
                Sel1_1Text.text = "에러\n1";
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
            OneTrackNode.SetActive(false);
            TwoTrackNode.SetActive(true);
            ThreeTrackNode.SetActive(false);

        if (curDay == 0) // 첫 전투
        {
            selDay1 = 1;    // 첫 전투 소모일 1 고정
            selDay2 = 1;

            BattleNode2_1.SetActive(true);
            EventNode2_1.SetActive(false);
            Sel2_1Text.text = "전투\n" + selDay1;

            BattleNode2_2.SetActive(true);
            EventNode2_2.SetActive(false);
            Sel2_2Text.text = "전투\n" + selDay2;
        }
        else // 이외
        {
            selDay1 = Random.Range(0, 3) + 1;   // 날짜 랜덤 세팅
            if (selEvnt1 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3) // 이벤트 3번 연속이면 무조건 전투
            {
                BattleNode2_1.SetActive(true);
                EventNode2_1.SetActive(false);
                Sel2_1Text.text = "전투\n" + selDay1;
            }
            else
            {
                BattleNode2_1.SetActive(false);
                EventNode2_1.SetActive(true);
                Sel2_1Text.text = "이벤트\n" + selDay1;
            }

            selDay2 = Random.Range(0, 3) + 1;
            if (selEvnt2 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                BattleNode2_2.SetActive(true);
                EventNode2_2.SetActive(false);
                Sel2_2Text.text = "전투\n" + selDay2;
            }
            else
            {
                BattleNode2_2.SetActive(false);
                EventNode2_2.SetActive(true);
                Sel2_2Text.text = "이벤트\n" + selDay2;
            }
        }
    }

    void ThreeTrackSet()    // 세갈래길 세팅
    {
            OneTrackNode.SetActive(false);
            TwoTrackNode.SetActive(false);
            ThreeTrackNode.SetActive(true);

        if (curDay == 0) // 첫 전투
        {
            selDay1 = 1;
            selDay2 = 1;
            selDay3 = 1;

            BattleNode3_1.SetActive(true);
            EventNode3_1.SetActive(false);
            Sel3_1Text.text = "전투\n" + selDay1;

            BattleNode3_2.SetActive(true);
            EventNode3_2.SetActive(false);
            Sel3_2Text.text = "전투\n" + selDay2;

            BattleNode3_3.SetActive(true);
            EventNode3_3.SetActive(false);
            Sel3_3Text.text = "전투\n" + selDay3;
        }
        else // 이외
        {
            selDay1 = Random.Range(0, 3) + 1;   // 날짜 랜덤 세팅
            if (selEvnt1 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                BattleNode3_1.SetActive(true);
                EventNode3_1.SetActive(false);
                Sel3_1Text.text = "전투\n" + selDay1;
            }
            else
            {
                BattleNode3_1.SetActive(false);
                EventNode3_1.SetActive(true);
                Sel3_1Text.text = "이벤트\n" + selDay1;
            }

            selDay2 = Random.Range(0, 3) + 1;
            if (selEvnt2 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                    BattleNode3_2.SetActive(true);
                    EventNode3_2.SetActive(false);
                Sel3_2Text.text = "전투\n" + selDay2;
            }
            else
            {
                    BattleNode3_2.SetActive(false);
                    EventNode3_2.SetActive(true);
                Sel3_2Text.text = "이벤트\n" + selDay2;
            }

            selDay3 = Random.Range(0, 3) + 1;
            if (selEvnt3 == 0 || saveManager.gameData.heroSaveData.EvntStream == 3)
            {
                BattleNode3_3.SetActive(true);
                EventNode3_3.SetActive(false);
                Sel3_3Text.text = "전투\n" + selDay3;
            }
            else
            {
                BattleNode3_3.SetActive(false);
                EventNode3_3.SetActive(true);
                Sel3_3Text.text = "이벤트\n" + selDay3;
            }
        }
    }

    public void Select1Btn()   // 1번 선택지
    {
        map.level = curDay;
        saveManager.gameData.heroSaveData.curDay += selDay1;
        if(selEvnt1 == 0)   // 전투
            saveManager.gameData.heroSaveData.EvntStream = 0;   // 초기화
        else if(selEvnt1 == 1)  // 이벤트
            saveManager.gameData.heroSaveData.EvntStream++; // 추가
        // 다음 단계 이동 => by 노드
    }

    public void Select2Btn()   // 2번 선택지
    {
        map.level = curDay;
        saveManager.gameData.heroSaveData.curDay += selDay2;
        if (selEvnt2 == 0)
            saveManager.gameData.heroSaveData.EvntStream = 0;
        else if (selEvnt2 == 1)
            saveManager.gameData.heroSaveData.EvntStream++;
        // 다음 단계 이동 => by 노드
    }

    public void Select3Btn()   // 3번 선택지
    {
        map.level = curDay;
        saveManager.gameData.heroSaveData.curDay += selDay3;
        if (selEvnt3 == 0)
            saveManager.gameData.heroSaveData.EvntStream = 0;
        else if (selEvnt3 == 1)
            saveManager.gameData.heroSaveData.EvntStream++;
        // 다음 단계 이동 => by 노드
    }
}