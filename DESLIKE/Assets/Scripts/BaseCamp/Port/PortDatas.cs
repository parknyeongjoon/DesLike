using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortDatas", menuName = "ScriptableObject/PortDatas")]
public class PortDatas : ScriptableObject
{
    public PortData[] portDatas = new PortData[30];
    public int curBarrierStrength, maxBarrierStrength;
    public List<HeroInfo> spawnSoldierList = new List<HeroInfo>();//?
    public List<HeroInfo> meleeSoldierList = new List<HeroInfo>();//?
    public List<HeroInfo> rangerSoldierList = new List<HeroInfo>();//?
    public List<HeroInfo> catapultSoldierList = new List<HeroInfo>();//?
    public Dictionary<string, SoldierData> activeSoldierList = new Dictionary<string, SoldierData>();
}
