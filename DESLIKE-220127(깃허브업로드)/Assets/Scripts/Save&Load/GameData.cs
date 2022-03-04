using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroSaveData
{
    public HeroData heroData;
    public float cur_Hp;
    public float cur_Mp;
    public bool resurrection;
    public int curDay;  // �߰�(by ����), �� ��¥ üũ��
    public int EvntStream;  // �߰�(by ����), �̺�Ʈ 3�� ���� �� ���� ����
    public int EvntCount;   // '���� - �̺�Ʈ - ����'���� �̺�Ʈ Ƚ��
    public bool midBossCheck = false, villageCheck = false, organCheck = false; // �߰� ����, ����, ���� ����
}

[System.Serializable]
public class SoldierSaveData
{
    public string soldierCode;
    public GameObject mutant;
    public int remain;
}

[System.Serializable]
public class GameData
{
    public bool canContinue;
    public GoodsCollection goodsCollection;//�ȵ�
    public Map map;//�ȵ�
    public PortDatas allyPortDatas;//�ȵ�
    public List<SoldierSaveData> soldierSaveList;
    public HeroSaveData heroSaveData;
    //mutant, ����, extraSkills �� �ٲ� �ֵ� �����ؾ���(continue ������ �����ϴ� ������ �ϸ� �� ��)
}
