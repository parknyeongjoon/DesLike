using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Installation",menuName ="ScriptableObject/ExtraSkill/Installation")]
public class Installation : SkillData
{
    [SerializeField] GameObject installPrefab;
    public float installTime;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        Destroy(Instantiate(installPrefab, targetInfo.transform.position, Quaternion.identity, heroInfo.transform),installTime);
    }
}
