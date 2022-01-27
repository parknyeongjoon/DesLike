using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EventNode", menuName = "ScriptableObject/MapNodeT/EventNode")]
public class EventNode : MapNode
{
    [SerializeField]
    string[] eventNames;
    public string eventName;

    public void SetEvent()
    {
        int temp = Random.Range(0, eventNames.Length);
        eventName = eventNames[temp];
    }

    public void Play_EventNode()
    {
        if (isPlayable)
        {
            map.curMapNode = this;
            map.playerX = x; map.playerY = y;
            map.CheckPlayableNode();
            SceneManager.LoadScene("Event");
        }
    }
}
