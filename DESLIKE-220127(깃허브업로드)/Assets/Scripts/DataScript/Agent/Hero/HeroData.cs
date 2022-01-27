using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/Agent/HeroData")]
[System.Serializable]
public class HeroData : CastleData
{
    public string soldier_name;
    public float mp, speed;
    public Tribe tribe;
    public GameObject basicAttack;
    public List<GameObject> extraSkill;
    public List<SkillData> skillList;
    public GameObject mutant;
}