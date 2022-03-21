using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSheet
{
    public Dictionary<string, SoldierData> soldierDataSheet = new Dictionary<string, SoldierData>();//�߰� ���� �߰��ϱ�
    public Dictionary<string, HeroData> heroDataSheet = new Dictionary<string, HeroData>();
    public Dictionary<string, SkillData> skillDataSheet = new Dictionary<string, SkillData>();
    public List<GameObject> relicObjectSheet;
    public List<RelicData> relicDataSheet;//Object���� ������ ��� ã��
    public List<GameObject> mutantObjectSheet;
    public List<MutantData> mutantDataSheet;//Object���� ������ ��� ã��
}
