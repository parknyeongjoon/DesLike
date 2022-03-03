using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortDatas", menuName = "ScriptableObject/PortDatas")]
public class PortDatas : ScriptableObject
{
    public PortData[] portDatas = new PortData[50];
    public List<SoldierInfo> spawnSoldierList = new List<SoldierInfo>();
    public Dictionary<string, SoldierData> activeSoldierList = new Dictionary<string, SoldierData>();
    public float cur_producetime;
    public float produceTime = 15.0f;
    public int round;
    public int max_round = 10;
}
