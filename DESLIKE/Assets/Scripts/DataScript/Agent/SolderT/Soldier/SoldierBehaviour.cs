using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : SoldierBasic//detect 함수 손보기
{
    new IEnumerator Start()
    {
        curSoundWeight = basicSoundWeight;
        yield return base.Start();
    }

    void FixedUpdate()
    {
        if (heroInfo.target)
        {
            heroInfo.moveDir = (heroInfo.target.transform.position - transform.position).normalized;
            if (heroInfo.moveDir.x > 0 && !heroInfo.skeletonAnimation.skeleton.FlipX)//유닛이 오른쪽으로 이동중이면서 유닛이 왼쪽을 보고 있다면
            {
                heroInfo.skeletonAnimation.skeleton.FlipX = true;
            }
            else if(heroInfo.moveDir.x < 0 && heroInfo.skeletonAnimation.skeleton.FlipX)//유닛이 왼쪽으로 이동중이면서 유닛이 오른쪽을 보고 있다면
            {
                heroInfo.skeletonAnimation.skeleton.FlipX = false;
            }
        }
        else { heroInfo.moveDir = new Vector3(0, 0, 0); }
    }

    protected override IEnumerator Idle_Behaviour()
    {
        //rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (curSoundWeight > Random.Range(0, 100)) {
            //AkSoundEngine.PostEvent("T_" + heroInfo.castleData.code + "_Idle", gameObject);//활성화
            curSoundWeight = basicSoundWeight;
        }
        while (heroInfo.state == Soldier_State.Idle)
        {
            heroInfo.state = Soldier_State.Detect;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Detect_Behaviour());
    }

    protected override IEnumerator Detect_Behaviour()
    {
        Coroutine detectCoroutine = StartCoroutine(Detect());
        if (heroInfo.action != Soldier_Action.Move) { StartCoroutine(Move()); }
        while (heroInfo.state == Soldier_State.Detect)
        {
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(detectCoroutine);
        if(heroInfo.state == Soldier_State.Battle) { StartCoroutine(Battle_Behaviour()); }
    }

    protected IEnumerator Detect()
    {
        while (true)
        {
            atkDetect?.Invoke();
            skillDetect?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override IEnumerator Battle_Behaviour()
    {
        //rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        while (heroInfo.state == Soldier_State.Battle)
        {
            if (canSkill != null && canSkill.Invoke())
            {
                yield return StartCoroutine(skillHandler?.Invoke(heroInfo.skillTargetInfo));
            }
            else if (canAtk != null && canAtk.Invoke())
            {
                yield return StartCoroutine(atkHandler?.Invoke(heroInfo.targetInfo));
            }
            else//idle->detect->battle->idle 루프를 돌아서 메모리 낭비가 심함 해결책 찾기
            {
                heroInfo.state = Soldier_State.Idle;
            }
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle_Behaviour());
    }
}
