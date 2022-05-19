using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObject/ExtraSkill/BuffData")]
public class BuffData : SkillData//퍼센트로 버프 주는 법 생각해내기
{
    public Buff_Stat buff_Stat;
    public float buff_Time;
    public int max_Stack;
    public BuffType buffType;
    [System.NonSerialized]
    public string buffCode;//지우기

    protected void OnEnable()
    {
        if (buffType == BuffType.None) { buffCode = code; }
        else { buffCode = buffType.ToString(); }
    }

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//이런 식으로 효과는 밖으로 빼기
    {
        Coroutine tempCoroutine = targetInfo.StartCoroutine(BuffCoroutine(heroInfo, targetInfo));
        targetInfo.buffCoroutine[code].Add(tempCoroutine);
    }

    public virtual IEnumerator BuffCoroutine(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        CheckCoroutineList(targetInfo);

        targetInfo.buff_Stat.Add_Stat(buff_Stat);
        AddBuffPanel(targetInfo);

        yield return new WaitForSeconds(buff_Time);

        Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)
        RemoveBuffPanel(targetInfo);
    }

    void CheckCoroutineList(HeroInfo targetInfo)
    {
        if (!targetInfo.buffCoroutine.ContainsKey(code))//딕셔너리에 키가 없다면 코루틴 리스트 추가
        {
            targetInfo.buffCoroutine.Add(code, new List<Coroutine>());
        }
        if (targetInfo.buffCoroutine[code].Count >= max_Stack)//최대 스택 수 보다 많은 지 검사
        {
            targetInfo.StopCoroutine(targetInfo.buffCoroutine[code][0]);//제일 오래된 코루틴 정지시키고 갱신하기
            Remove_Buff(targetInfo, targetInfo.buffCoroutine[code][0]);//고치기(0번째 인덱스말고 실행된 코루틴을 담을 방법이 없을까?)//효과 제거해주기
        }
    }

    void AddBuffPanel(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.AddBuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.AddBuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
    }

    void RemoveBuffPanel(HeroInfo targetInfo)
    {
        if (targetInfo.gameObject.CompareTag("Player")) { BattleUIManager.Instance.heroPanel.RemoveBuff(code); }//영웅에게 버프를 줬다면 버프 패널 업데이트
        else if (targetInfo == BattleUIManager.Instance.cur_Soldier) { BattleUIManager.Instance.soldierPanel.RemoveBuff(code); }//현재 soldierPanel에서 보여주고 있는 병사라면 버프 패널 업데이트
    }

    public virtual void Remove_Buff(HeroInfo targetInfo, Coroutine coroutine)
    {
        targetInfo.buff_Stat.Remove_Stat(buff_Stat);
        targetInfo.buffCoroutine[code].Remove(coroutine);
    }
}
