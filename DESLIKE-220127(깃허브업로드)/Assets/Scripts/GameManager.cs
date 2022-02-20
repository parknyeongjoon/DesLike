using UnityEngine;
using UnityEngine.Events;

/*
* ///////////////////////////////////////////////////////////////////////////////////////////////////시스템 기획
* set_port_panel 수정하기
* 게임오버 만들기
* 사전 디테일에 스킬 추가
* 유닛에 move state 넣어서 그 동안 움직임 멈추기
* hp바 이상함
* setPort에서 데이터 표시
* 게임 클리어 만들기
* 영웅 프리팹을 하나하나 만드는 거 고려해봐야할 듯
* Time Scale 건드리기
* relic 중복해서 안 나오게 만들기
* grenade에서 single쓰면 마우스 포인터 안 바뀜
* 노드 한 번 선택하면 되돌리지는 못하게 하기
* GameData 추가 해야함
* 영웅 마나 text 안 뜸
* scene 움직일 때 null 뜨는 거 고치기
* reward에 챌린지 보상 추가
* soldier reward 주거나 적으로 나올 때 mutant 달고 나오게 할 방법 생각하기
* 객체 풀링 공부하기(필수)
* OnSceneLoaded를 GameManager에서 관리하기?
* 타일 굳이 isometric으로 할 이유가 없음
* continue할 때 gameData에 activeSoldierList에 있는 soldierData들은 값들 allSoldierList에 값 옮기기
* reward에 병사 누르면 여러 개 중 선택권 주기
* relic이나 mutant 등 없어질 때 disable로 효과 제거해주기
* 진행 중인 게임에 대한 데이터는 GameData에 저장하고 전체적인 데이터는 DataSheet에 저장하기
* DataSheet 온라인 서버에 연동하기(csv테이블 이용하기?)
* GameData 저장할때 activeSoldierList에 있는 애들 soldierSaveData에 넣기, 불러오는 기능 만들기
* 라운드 UI 시간 부분 다시 만지기
* 버프, 디버프 중첩 가능하게 만들고 soldierPanel에서 볼 수 있게 만들기
* 때리다가 일정 범위 이상 벗어나면 그만 쫒아오게 하기
* gamePause동안 스킬 못 쓰고 못 움직이게 하기
* 병사 체력, 마나 리젠 만들기
* HeroSkillUse 자동이동 버그 있음, grenadeSkill은 타켓팅 설정시 범위 보여주기
* ///////////////////////////////////////////////////////////////////////////////////////////////컨텐츠 기획
* 방어력 수식 만들기
* 배틀 중간중간에도 이벤트 나오게 하기(배틀의 템포를 끊을 것 같으므로 보류)
* 진영 별로 특수효과 만들기
* 숄져 데이터 더 만들기
* 마을 컨텐츠 추가하기
* 유물 아이템 만들기
* 덱 빌딩 로그라이트 장르인데 덱이 방대하게 커지는 거에 대한 리스크가 없음(최대 유닛 수 제한?)(유닛의 등급의 합의 제한) 본거지에서 제한 풀기
* 재생, 오염 등 특수 단어에 대해서 Dictionary<stirng, string>으로 저장해놓고 마우스 오버 시 불러오기
* //////////////////////////////////////////////////////////////////////////////////////////////데모이후
* 지형 효과 만들기(보류)
  //////////////////////////////////////////////////////////////////////////////////////////////메모장
  ///
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
            GamePause();
        }
    }

    //일시정지
    void GamePause()
    {
        gamePause = !gamePause;
        if (gamePause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
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