using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "EventNode", menuName = "ScriptableObject/MapNodeT/EventNode")]
public class EventNode : MapNode
{
    [SerializeField] string[] eventNames;
    public string eventName;
    bool CheckSet= false;

    public void SetEvent()
    {
        if (CheckSet == false)
        {
            int temp = Random.Range(0, eventNames.Length);
            eventName = eventNames[temp];
            CheckSet = true;
        }
    }

    public void Play_EventNode()
    {
        map.curMapNode = this;
        SceneManager.LoadScene("Event");
    }
}
