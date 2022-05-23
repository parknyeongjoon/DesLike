using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Relic_Physic_E_4",menuName ="ScriptableObject/RelicT/Physic/Relic_Physic_E_4")]
public class Relic_Physic_E_4 : InstanceRelicData
{
    public override void Effect()
    {
        SaveManager.Instance.dataSheet.heroDataSheet[SaveManager.Instance.gameData.heroSaveData.heroCode].extraSkills += RelicEffect;
    }

    void RelicEffect(HeroInfo heroInfo)
    {

    }
}
