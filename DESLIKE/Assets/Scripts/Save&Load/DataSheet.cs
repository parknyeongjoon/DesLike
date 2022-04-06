using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSheet
{
    public Dictionary<string, SoldierData> soldierDataSheet = new Dictionary<string, SoldierData>();//발견 여부 추가하기
    public Dictionary<string, HeroData> heroDataSheet = new Dictionary<string, HeroData>();
    public Dictionary<string, SkillData> skillDataSheet = new Dictionary<string, SkillData>();
    public Dictionary<string, MutantData> mutantDataSheet = new Dictionary<string, MutantData>();
    public Dictionary<string, RelicData> relicDataSheet = new Dictionary<string, RelicData>();
    public List<GameObject> relicObjectSheet;
    public List<GameObject> mutantObjectSheet;
}
