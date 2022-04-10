using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EventNode", menuName = "ScriptableObject/MapNodeT/EventNode")]
public class EventNode : MapNode
{
    [SerializeField] string[] eventNames;
    public string eventName;
    const int THREE = 3;
    public bool[] isEventSet = new bool[THREE];
    public bool[] isRewardSet = new bool[THREE];
    public SoldierReward soldierReward = new SoldierReward();

    public void Play_EventNode()
    {
        map.curMapNode = this;
        SceneManager.LoadScene("Event");
    }
}
