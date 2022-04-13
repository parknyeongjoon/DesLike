using UnityEngine;
using UnityEngine.Events;

/*
* ///////////////////////////////////////////////////////////////////////////////////////////////////시스템 기획
* set_port_panel에 정보 추가하기
* 게임오버 만들기
* 사전 디테일에 스킬 추가
* setPort에서 데이터 표시
* 게임 클리어 만들기
* ///영웅 프리팹을 하나하나 만드는 거 고려해봐야할 듯
* Time Scale 건드리기
* //relic 중복해서 안 나오게 만들기
* 노드 한 번 선택하면 되돌리지는 못하게 하기
* GameData 추가 해야함
* 객체 풀링 공부하기(필수)
* //continue할 때 gameData에 activeSoldierList에 있는 soldierData들은 값들 allSoldierList에 값 옮기기
* //reward에 병사 누르면 여러 개 중 선택권 주기
* //relic이나 mutant 등 없어질 때 disable로 효과 제거해주기
* //진행 중인 게임에 대한 데이터는 GameData에 저장하고 전체적인 데이터는 DataSheet에 저장하기
* //DataSheet 온라인 서버에 연동하기(csv테이블 이용하기?보류)
* ///GameData 저장할때 activeSoldierList에 있는 애들 soldierSaveData에 넣기, 불러오는 기능 만들기
* gamePause동안 스킬 못 쓰고 못 움직이게 하기
* ///atkDetect랑 skillDetect 둘 다 있으면 skillDetect만 사용됨(target 분리해서 해결)
* CastleInfo랑 HeroInfo, CastleData랑 HeroData 통합하기(아. 아. 아. 언제 함 아.)
* 캐릭터들 체력바 soldierInfo에 OnDamaged 될 때만 넣어주기, 체젠에도 반영이 돼야 함
* //도발 기능 만들기(Grenade Debuff로)//보류
* relic save&load 만들기
* 죽으면 체마나젠 멈추기, 영웅 조작 막기
* 사운드 가중치 함수로 빼기
* 전투 시작 병사별로 조금 다르게 하기
* soldier가 버프주는 상황 생각하기
* 영웅 세이브 데이터 이어지나 체크해보기
* 스턴 - 디버프 패널 아이콘 설정
* 영웅 스턴 중에 못 움직이게
* 스킬 스크립트들 더 합치기
* ///////////////////////////////////////////////////////////////////////////////////////////////컨텐츠 기획
* 방어력 수식 만들기
* 배틀 중간중간에도 이벤트 나오게 하기(배틀이 한 라운드로 바뀌면서 취소)
* 진영 별로 특수효과 만들기
* 숄져 데이터 더 만들기
* 마을 컨텐츠 추가하기
* 유물 아이템 만들기
* 덱 빌딩 로그라이트 장르인데 덱이 방대하게 커지는 거에 대한 리스크가 없음(최대 유닛 수 제한)(유닛의 등급의 합의 제한) 본거지에서 제한 풀기 - 해결
* 재생, 오염 등 특수 단어에 대해서 Dictionary<stirng, string>으로 저장해놓고 마우스 오버 시 불러오기
* 어그로 수준을 만들어야 하나? 때리다가 다른 더 위협적인 애를 때리는 수준으로 해야하나 고민
* //////////////////////////////////////////////////////////////////////////////////////////////데모이후
* 지형 효과 만들기(보류)
  //////////////////////////////////////////////////////////////////////////////////////////////메모장
*/
public class GameManager : MonoBehaviour
{
    static GameObject container;
    static GameObject Container
    {
        get
        {
            return container;
        }
    }

    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "GameManager";
                DontDestroyOnLoad(container);
                instance = container.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public bool gamePause;//게임 일시정지용

    public UnityEvent gameOverEvent;

    void Update()
    {
        //게임 일시정지
        if (Input.GetKeyDown(KeyCode.P))
        {
            GamePause(!gamePause);
        }
    }

    //일시정지
    public void GamePause(bool isPause)
    {
        gamePause = isPause;
        if (gamePause)
        {
            Time.timeScale = 0;
            AkSoundEngine.PostEvent("Game_Speed_Pause", gameObject);
        }
        else
        {
            Time.timeScale = 1;
            AkSoundEngine.PostEvent("Game_Speed_Resume", gameObject);
        }
    }

    public void timeScale0()
    {
        GamePause(true);
    }

    public void timeScale1()
    {
        GamePause(false);
    }

    public void timeScale2()
    {
        gamePause = false;
        Time.timeScale = 2;
    }

    public static void DeleteChilds(GameObject gameObject)
    {
        Transform[] childList = gameObject.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; i++)
            {
                if (childList[i] != gameObject.transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }
}