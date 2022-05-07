using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Installation",menuName ="ScriptableObject/ExtraSkill/Installation")]
public class Installation : SkillData
{
    [SerializeField] GameObject installPrefab;
    public float installTime;

    Skill skill;

    void OnEnable()
    {
        skill = installPrefab.GetComponent<Skill>();
    }

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        GameObject tempObject = Instantiate(installPrefab, targetInfo.transform.position, Quaternion.identity);
        tempObject.layer = heroInfo.gameObject.layer;
        Destroy(tempObject, installTime);
    }
}
