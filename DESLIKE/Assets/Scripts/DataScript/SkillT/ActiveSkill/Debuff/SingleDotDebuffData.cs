using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleDotDebuffData", menuName = "ScriptableObject/SkillT/SingleDotDebuffData")]
public class SingleDotDebuffData : SingleDebuffData
{
    public float dotTime;
    public float dotDamage;

    public override IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        Debug.Log(debuffCode);
        if (!targetInfo.debuffCoroutine.ContainsKey(debuffCode))//딕셔너리에 키가 없다면 코루틴 리스트 추가
        {
            Debug.Log("생성");
            targetInfo.debuffCoroutine.Add(debuffCode, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[debuffCode].Count >= max_Stack)//최대 스택 수 보다 많은 지 검사
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[debuffCode][0]);//제일 오래된 코루틴 정지시키고 갱신하기
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[debuffCode][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
        }

        float _debuff_Time = debuff_Time;
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(debuffCode); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(debuffCode); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        while (_debuff_Time >= dotTime)
        {
            targetInfo.OnDamaged(dotDamage);
            yield return new WaitForSeconds(dotTime);
            _debuff_Time -= dotTime;
        }
        yield return new WaitForSeconds(_debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(debuffCode); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(debuffCode); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
    }
}
