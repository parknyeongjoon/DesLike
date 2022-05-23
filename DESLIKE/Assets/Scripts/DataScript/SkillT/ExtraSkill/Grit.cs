using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grit",menuName ="ScriptableObject/ExtraSkill/Grit")]
public class Grit : SkillData
{
    public float activeHealthP;
    public float plusAtk;
    public float plusAtkSpeed;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.healthChangeEvent.AddListener(GritEffect);
    }

    void GritEffect(HeroInfo heroInfo)
    {
        if(heroInfo.cur_Hp <= heroInfo.castleData.hp * activeHealthP)
        {
            Debug.Log("투지 활성화");
            heroInfo.buff_Stat.atk += plusAtk;
            heroInfo.buff_Stat.atk_Speed += plusAtkSpeed;
            excuteExtraSkill(heroInfo, heroInfo);
            heroInfo.healthChangeEvent.RemoveListener(GritEffect);
        }
    }
}
