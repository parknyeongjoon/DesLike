using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : SoldierBasic
{
    Coroutine atkCoroutine;
    Coroutine moveCoroutine;

    new void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (atkCoroutine != null) { Debug.Log("atkCoroutine o"); }
        else { Debug.Log("atkCoroutine x"); }
        if (heroInfo.state < Soldier_State.Stun && heroInfo.action != Soldier_Action.End_Delay)//스턴, 도발, 사망, 엔드 딜레이 시에는 행동 불가
        {
            if (Input.GetMouseButtonDown(1))//우클릭시
            {
                //마우스 우클릭 시 Idle 사운드 가중치 증가
                curSoundWeight += 2;
                if (curSoundWeight > Random.Range(0, 100))
                {
                    AkSoundEngine.PostEvent(heroInfo.castleData.code + "_Idle", gameObject);
                    curSoundWeight = 10;
                }
                //현재 마우스 위치로 벡터 값 잡기
                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //평타 때릴 유닛 있나 탐색(중립 유닛과 적 유닛)
                Collider2D targetCollider = Physics2D.OverlapPoint(mousePos, ((int)heroInfo.team ^ (int)Team.Ally * 7) << 8);//8대신 Ground 들어가야함
                if (targetCollider)//있으면 평타 가능한 지 체크
                {
                    heroInfo.target = targetCollider.gameObject;//타켓 설정
                    heroInfo.targetInfo = targetCollider.GetComponent<HeroInfo>();//타켓 인포 받아오기
                    heroInfo.moveDir = (targetCollider.transform.position - transform.position).normalized;//move 방향 재 설정
                    if (heroInfo.action != Soldier_Action.Move) { moveCoroutine = StartCoroutine(Move()); }//moveCoroutine이 없다면 실행
                    if (atkCoroutine == null) { atkCoroutine = StartCoroutine(MoveToAtk()); }//atkCoroutine이 없다면 실행
                }
                else//없으면 이동
                {
                    if (atkCoroutine != null) { StopCoroutine(atkCoroutine); }//실행중인 atkCoroutine이 있다면 중지
                    if (moveCoroutine != null) { StopCoroutine(moveCoroutine); }//실행중인 moveCoroutine이 있다면 중지
                    heroInfo.moveDir = (new Vector3(mousePos.x, mousePos.y, 0) - transform.position).normalized;//move 방향 재 설정
                    if (heroInfo.action != Soldier_Action.Move) { moveCoroutine = StartCoroutine(Move(mousePos)); }//moveCoroutine이 없다면 실행
                }
            }
        }
    }

    IEnumerator MoveToAtk()
    {
        while (true)
        {
            if(heroInfo.action != Soldier_Action.Move) { break; }//action이 idle이 되었다면 종료
            if (canAtk != null && canAtk.Invoke())
            {
                while (canAtk.Invoke())
                {
                    yield return StartCoroutine(atkHandler?.Invoke(heroInfo.targetInfo));
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
