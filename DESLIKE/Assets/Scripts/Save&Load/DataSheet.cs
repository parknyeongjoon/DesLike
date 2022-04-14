using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSheet
{
    public Dictionary<string, SoldierData> soldierDataSheet = new Dictionary<string, SoldierData>();
    public Dictionary<string, HeroData> heroDataSheet = new Dictionary<string, HeroData>();
    public Dictionary<string, SkillData> skillDataSheet = new Dictionary<string, SkillData>();
    public Dictionary<string, MutantData> mutantDataSheet = new Dictionary<string, MutantData>();
    public Dictionary<string, GameObject> mutantObjectSheet = new Dictionary<string, GameObject>();
    public Dictionary<string, RelicData> relicDataSheet = new Dictionary<string, RelicData>();
    public Dictionary<string, GameObject> relicObjectSheet = new Dictionary<string, GameObject>();
}