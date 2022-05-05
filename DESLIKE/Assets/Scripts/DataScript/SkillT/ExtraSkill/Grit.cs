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
            if (!heroInfo.grit)//grit이 활성화가 안 되어있었다면 활성화해주고 버프
            {
                Debug.Log("투지 활성화");
                heroInfo.grit = true;
                heroInfo.buff_Stat.atk += plusAtk;
                heroInfo.buff_Stat.atk_Speed += plusAtkSpeed;
            }
        }
        else
        {
            if(heroInfo.grit)//grit 활성화 되어있었다면 비활성화해주고 디버프
            {
                Debug.Log("투지 비활성화");
                heroInfo.grit = false;
                heroInfo.buff_Stat.atk -= plusAtk;
                heroInfo.buff_Stat.atk_Speed -= plusAtkSpeed;
            }
        }
    }
}
