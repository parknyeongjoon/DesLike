using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/Agent/HeroData")]
[System.Serializable]
public class HeroData : CastleData
{
    public string soldier_name;
    public float mp, speed, mp_Re, hp_Re;
    public Tribe tribe;
    public List<GameObject> extraSkill;
    public GameObject mutant;
}