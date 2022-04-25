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
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[code].Count >= max_Stack)//최대 스택 수 보다 많은 지 검사
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[code][0]);//제일 오래된 코루틴 정지시키고 갱신하기, 버그 일어남
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
        }

        float _debuff_Time = debuff_Time;
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        while (_debuff_Time >= dotTime)
        {
            targetInfo.OnDamaged(dotDamage);
            yield return new WaitForSeconds(dotTime);
            _debuff_Time -= dotTime;
        }
        yield return new WaitForSeconds(_debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
    }
}
