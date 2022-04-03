using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganNodeScript : NodeScript
{
    OrganNode organNode;

    void Start()
    {
        organNode = (OrganNode)MapNode;
    }

    public void Play_OrganNode()
    {
        organNode.Play_OrganNode();
    }
}
