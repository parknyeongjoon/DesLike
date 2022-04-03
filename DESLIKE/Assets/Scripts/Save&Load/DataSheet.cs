using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSheet
{
    public Dictionary<string, SoldierData> soldierDataSheet = new Dictionary<string, SoldierData>();//발견 여부 추가하기
    public Dictionary<string, HeroData> heroDataSheet = new Dictionary<string, HeroData>();
    public Dictionary<string, SkillData> skillDataSheet = new Dictionary<string, SkillData>();
    public List<GameObject> relicObjectSheet;
    public List<RelicData> relicDataSheet;//Object에서 가져올 방법 찾기
    public List<GameObject> mutantObjectSheet;
    public List<MutantData> mutantDataSheet;//Object에서 가져올 방법 찾기
}
