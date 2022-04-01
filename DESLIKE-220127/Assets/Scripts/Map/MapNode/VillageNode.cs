using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "VillageNode", menuName = "ScriptableObject/MapNodeT/VillageNode")]
public class VillageNode : MapNode
{
    public void Play_VillageNode()
    {
        // if (isPlayable)
        // {
            map.playerX = x; map.playerY = y;
            // map.CheckPlayableNode();
            SceneManager.LoadScene("Village");
        // }
    }
}