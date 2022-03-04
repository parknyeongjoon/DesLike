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
    public int[] eventableDot = new int[3]; // 의미 없음
    public bool isVisited = false;  // 의미 없음
    public List<SoldierData> rewardSoldierList;
    public List<GameObject> rewardRelicList;
    public Reward reward;
    public int x, y;    // 잘 모르겠음
}