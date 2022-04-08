using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EventNode", menuName = "ScriptableObject/MapNodeT/EventNode")]
public class EventNode : MapNode
{
    [SerializeField] string[] eventNames;
    public string eventName;
    public bool[] isEventSet = new bool[3];
    public bool isRewardSet;
    public SoldierReward soldierReward = new SoldierReward();

    public void Play_EventNode()
    {
        map.curMapNode = this;
        SceneManager.LoadScene("Event");
    }
}
