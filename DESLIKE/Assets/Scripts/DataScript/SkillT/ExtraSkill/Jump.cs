using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : SkillData
{
    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        if(targetInfo.allyPortDatas.rangerSoldierList.Count > 0)//원거리 진영에 남은 병사가 있다면
        {
            int temp = Random.Range(0, targetInfo.allyPortDatas.rangerSoldierList.Count);
            heroInfo.StartCoroutine(JumpEffect(heroInfo, targetInfo.allyPortDatas.rangerSoldierList[temp]));
        }
        else if(targetInfo.allyPortDatas.meleeSoldierList.Count > 0)//근거리 진영에 남은 병사가 있다면
        {
            int temp = Random.Range(0, targetInfo.allyPortDatas.meleeSoldierList.Count);
            heroInfo.StartCoroutine(JumpEffect(heroInfo, targetInfo.allyPortDatas.meleeSoldierList[temp]));
        }
    }

    IEnumerator JumpEffect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        float time = 0;
        Vector3 startPos = targetInfo.transform.position;
        Vector3 endPos = targetInfo.transform.position;
        while (time <= 1.5f)
        {
            time += Time.deltaTime;
            heroInfo.transform.position = Vector3.Lerp(startPos, endPos, time / 1.5f);
            yield return new WaitForFixedUpdate();
        }
    }
}
