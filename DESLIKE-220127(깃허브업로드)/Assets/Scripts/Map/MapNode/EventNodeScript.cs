using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeScript : NodeScript
{
    EventNode eventNode;
    [SerializeField] GameObject InfoTemp;

    void Start()
    {
        eventNode = (EventNode)MapNode;
        eventNode.SetEvent();
    }

    public void See_Event()
    {
        GameManager.DeleteChilds(InfoTemp);
    }

    public void Play_EventNode()
    {
        eventNode.Play_EventNode();
    }
 }
