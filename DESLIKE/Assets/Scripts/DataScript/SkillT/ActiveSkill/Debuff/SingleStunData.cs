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
        Debug.Log("스턴");
        yield return null;
    }
}
