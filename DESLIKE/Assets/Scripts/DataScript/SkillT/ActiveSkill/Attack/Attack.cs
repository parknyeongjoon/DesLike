using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public override IEnumerator UseSkill(HeroInfo targetInfo)
    {
        heroInfo.action = Soldier_Action.Skill;
        if (heroInfo.skeletonAnimation.skeleton != null)//지우기
            heroInfo.skeletonAnimation.state.SetAnimation(0, skillData.code, false);//스킬
        //AkSoundEngine.PostEvent(skillData.code, gameObject);활성화
        yield return new WaitForSeconds(((ActiveSkillData)skillData).start_Delay);
        if (targetInfo && targetInfo.gameObject.layer != 7)
        {
            heroInfo.cur_Mp -= ((ActiveSkillData)skillData).mp;
            cur_cooltime = ((ActiveSkillData)skillData).cooltime;
            StartCoroutine(SkillCooltime());
            skillData.Effect(heroInfo, targetInfo);
            Debug.Log("유즈 스킬");
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(((ActiveSkillData)skillData).end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }
}
