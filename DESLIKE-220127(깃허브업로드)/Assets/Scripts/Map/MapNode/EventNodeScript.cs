using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeScript : NodeScript
{
    EventNode eventNode;

    void Start()
    {
        eventNode = (EventNode)MapNode;
        eventNode.SetEvent();
    }

    public void Play_EventNode()
    {
        eventNode.Play_EventNode();
    }
 }
