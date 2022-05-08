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
            if (!heroInfo.grit)//grit�� Ȱ��ȭ�� �� �Ǿ��־��ٸ� Ȱ��ȭ���ְ� ����
            {
                Debug.Log("���� Ȱ��ȭ");
                heroInfo.grit = true;
                heroInfo.buff_Stat.atk += plusAtk;
                heroInfo.buff_Stat.atk_Speed += plusAtkSpeed;
            }
        }
        else
        {
            if(heroInfo.grit)//grit Ȱ��ȭ �Ǿ��־��ٸ� ��Ȱ��ȭ���ְ� �����
            {
                Debug.Log("���� ��Ȱ��ȭ");
                heroInfo.grit = false;
                heroInfo.buff_Stat.atk -= plusAtk;
                heroInfo.buff_Stat.atk_Speed -= plusAtkSpeed;
            }
        }
    }
}
