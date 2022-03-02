using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject oneTrack, twoTrack, threeTrack;
    [SerializeField]
    Text HPText, Sel1_1Text, Sel2_1Text, Sel2_2Text, Sel3_1Text, Sel3_2Text, Sel3_3Text, CurrentDayText;

    GameObject hero;
    HeroInfo heroInfo;
    int curDay = 1; // 현재 날짜
    int sel1Day, sel2Day, sel3Day;  // 소모 날짜
    int sel1Evnt, sel2Evnt, sel3Evnt;   // 다음 단계 결정
    bool midBossCheck = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부

    void Start()
    {
        CommonSetting();
        int mapSelect = 0;

        if (sel1Evnt != 0 && sel1Evnt != 1) // 일반적인 상황(이벤트 or 전투)이 아니라면
            mapSelect = 3;  // 외길
        else mapSelect = Random.Range(0, 2); // 일반적인 상황(이벤트 or 전투)이면 두갈래길과 세갈래길 결정

        if (mapSelect == 0)
        {
            oneTrack.SetActive(false);
            twoTrack.SetActive(true);
            threeTrack.SetActive(false);
            TwoTrackSet();
        }
        else if(mapSelect == 1)
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
        if (curDay == 0) // 첫 라운드는 무조건 전투
        {
            sel1Evnt = 0;
            sel2Evnt = 0;
            sel3Evnt = 0;
        }
        else if((curDay >= 14) && (midBossCheck == false))  // 중간 보스 라운드
        {
            sel1Evnt = 2;
            sel2Evnt = 2;
            sel3Evnt = 2;
            midBossCheck = true;
        }
        else if ((curDay >= 15) && (villageCheck == false))  // 중간 보스 라운드
        {   // 중간 보스 다음에 바로 마을 붙이면 필요없나?
            sel1Evnt = 3;
            sel2Evnt = 3;
            sel3Evnt = 3;
            villageCheck = true;
        }
        else if ((curDay >= 29) && (villageCheck == false))  // 정비 라운드
        { 
            sel1Evnt = 4;
            sel2Evnt = 4;
            sel3Evnt = 4;
            organCheck = true;
        }
        else if(organCheck == true) // 최종 보스 라운드
        {
            sel1Evnt = 5;
            sel2Evnt = 5;
            sel3Evnt = 5;
        }
        else    // 일반 적인 경우 0 : 전투, 1 : 이벤트 
        {
            sel1Evnt = Random.Range(0, 2);
            sel2Evnt = Random.Range(0, 2);
            sel3Evnt = Random.Range(0, 2);
        }
    }

    void OneTrackSet()  // 외길 세팅
    {
        sel1Day = 1;   // 날짜 1로 고정
        switch(sel1Evnt)
        {
            case 2: // 중간 보스
                Sel1_1Text.text = "중간 보스\n1";
                break;
            case 3: // 마을
                Sel1_1Text.text = "마을\n1";
                break;
            case 4: // 정비
                Sel1_1Text.text = "정비\n1";
                break;
            case 5: // 최종 보스
                Sel1_1Text.text = "최종 보스\n1";
                break;
            default:
                Sel1_1Text.text = "에러\n1";
                break;
        }
    }

    void TwoTrackSet()  // 두갈래길 세팅
    {
        if (curDay == 0) // 첫 전투
        {
            sel1Day = 1;
            sel2Day = 1;
            Sel2_1Text.text = "전투\n" + sel1Day;
            Sel2_2Text.text = "전투\n" + sel2Day;
        }
        else // 이외
        {
            sel1Day = Random.Range(0, 3) + 1;   // 날짜 랜덤 세팅
            if (sel1Evnt == 0)
                Sel2_1Text.text = "전투\n" + sel1Day;
            else Sel2_1Text.text = "이벤트\n" + sel1Day;

            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
                Sel2_2Text.text = "전투\n" + sel2Day;
            else Sel2_2Text.text = "이벤트\n" + sel2Day;
        }
    }

    void ThreeTrackSet()    // 세갈래길 세팅
    {
        if (curDay == 0) // 첫 전투
        {
            sel1Day = 1;
            sel2Day = 1;
            sel3Day = 1;
            Sel3_1Text.text = "전투\n" + sel1Day;
            Sel3_2Text.text = "전투\n" + sel2Day;
            Sel3_3Text.text = "전투\n" + sel3Day;
        }
        else // 이외
        {
            sel1Day = Random.Range(0, 3) + 1;   // 날짜 랜덤 세팅
            if (sel1Evnt == 0)
                Sel3_1Text.text = "전투\n" + sel1Day;
            else Sel3_1Text.text = "이벤트\n" + sel1Day;

            sel2Day = Random.Range(0, 3) + 1;
            if (sel2Evnt == 0)
                Sel3_2Text.text = "전투\n" + sel2Day;
            else Sel3_2Text.text = "이벤트\n" + sel2Day;

            sel3Day = Random.Range(0, 3) + 1;
            if (sel3Evnt == 0)
                Sel3_3Text.text = "전투\n" + sel1Day;
            else Sel3_3Text.text = "이벤트\n" + sel1Day;
        }
    }

    void Select1Btn()   // 1번 선택지
    {
        curDay += sel1Day;
        // 다음 단계 이동
    }

    void Select2Btn()   // 2번 선택지
    {
        curDay += sel2Day;
        // 다음 단계 이동
    }

    void Select3Btn()   // 3번 선택지
    {
        curDay += sel2Day;
        // 다음 단계 이동
    }
}