using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSheet
{
    public Dictionary<string, SoldierData> soldierDataSheet;//�߰� ���� �߰��ϱ�
    public Dictionary<string, HeroData> heroDataSheet;
    public Dictionary<string, SkillData> skillDataSheet;
    public List<GameObject> relicObjectSheet;
    public List<RelicData> relicDataSheet;//Object���� ������ ��� ã��
    public List<GameObject> mutantObjectSheet;
    public List<MutantData> mutantDataSheet;//Object���� ������ ��� ã��
}
