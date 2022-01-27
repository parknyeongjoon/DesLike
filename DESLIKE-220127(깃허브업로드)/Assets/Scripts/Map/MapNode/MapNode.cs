using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapNode", menuName = "ScriptableObject/MapNode")]
public class MapNode : ScriptableObject
{
    public Map map;
    public bool isNode = false;
    public bool isPlayable = false;
    public bool[] isPath = new bool[3];
    public bool isVisited = false;
    public List<SoldierData> rewardSoldierList;
    public List<GameObject> rewardRelicList;
    public Reward reward;
    public int x, y;
}