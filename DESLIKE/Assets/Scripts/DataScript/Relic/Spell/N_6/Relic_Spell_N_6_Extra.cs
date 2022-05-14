using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic_Spell_N_6_Extra", menuName = "ScriptableObject/RelicT/Extra/Relic_Spell_N_6_Extra")]
public class Relic_Spell_N_6_Extra : SkillData//������ �Ǻΰ� �����Ǵ� ���� �ɷ�ġ ���
{
    [SerializeField] Buff_Stat buff_Stat;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)//Ÿ���� mucus�� �����ٸ� ������ �߰����ְ� mucus�� 0���� �˻����ִ� �Լ��� �ǰ��Լ��� �߰�
    {
        if(targetInfo.mucus == 0)
        {
            targetInfo.buff_Stat.Add_Stat(buff_Stat);//���� ������
            targetInfo.afterHitEvent += RemoveEffect;//�ǰ� �̺�Ʈ ������
        }
    }

    void RemoveEffect(HeroInfo heroInfo, HeroInfo targetInfo, float damage)//�ǰ� �̺�Ʈ
    {
        if(heroInfo.mucus == 0)
        {
            heroInfo.buff_Stat.Remove_Stat(buff_Stat);
            targetInfo.afterHitEvent -= RemoveEffect;
        }
    }
}
