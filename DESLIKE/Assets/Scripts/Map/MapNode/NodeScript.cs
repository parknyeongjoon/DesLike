using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;
    protected Image nodeImg;
    public MapNode MapNode { get => mapNode; set => mapNode = value; }

}