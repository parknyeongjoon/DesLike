using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleStunData", menuName = "ScriptableObject/SkillT/SingleStunData")]
public class SingleStunData : SingleDebuffData
{
    public override IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }

        //스턴 아이콘 설정
        targetInfo.Stun(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        yield return null;
    }
}
