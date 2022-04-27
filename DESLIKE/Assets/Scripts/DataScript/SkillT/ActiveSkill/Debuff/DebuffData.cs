using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffData", menuName = "ScriptableObject/extraSkill/DebuffData")]
public class DebuffData : SkillData
{
    public Buff_Stat debuff_Stat;
    public float debuff_Time;
    public int max_Stack;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        Coroutine tempCoroutine = targetInfo.StartCoroutine(DebuffCoroutine(heroInfo, targetInfo));
        targetInfo.debuffCoroutine[code].Add(tempCoroutine);
    }

    public virtual IEnumerator DebuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if (!targetInfo.debuffCoroutine.ContainsKey(code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
        {
            targetInfo.debuffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.debuffCoroutine[code].Count >= max_Stack)//최대 스택 수 보다 많은 지 검사
        {
            targetInfo.StopCoroutine(targetInfo.debuffCoroutine[code][0]);//제일 오래된 코루틴 정지시키고 갱신하기
            Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
        }

        targetInfo.buff_Stat.Add_Stat(debuff_Stat);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        yield return new WaitForSeconds(debuff_Time);
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveDebuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveDebuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
        Remove_Debuff(targetInfo, targetInfo.debuffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)
    }

    protected virtual void Remove_Debuff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(debuff_Stat);
        targetInfo.debuffCoroutine[code].Remove(coroutine);
    }
}
