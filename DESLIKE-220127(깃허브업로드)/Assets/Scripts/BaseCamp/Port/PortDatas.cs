using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortDatas", menuName = "ScriptableObject/PortDatas")]
public class PortDatas : ScriptableObject
{
    public PortData[] portDatas = new PortData[30];
    public int curBarrierStrength, maxBarrierStrength;
    public List<SoldierInfo> spawnSoldierList = new List<SoldierInfo>();
    public Dictionary<string, SoldierData> activeSoldierList = new Dictionary<string, SoldierData>();
}
