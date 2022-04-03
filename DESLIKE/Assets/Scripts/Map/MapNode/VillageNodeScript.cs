using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageNodeScript : NodeScript
{
    VillageNode villageNode;

    void Start()
    {
        villageNode = (VillageNode)MapNode;
    }

    public void Play_VillageNode()
    {
        villageNode.Play_VillageNode();
    }
}
