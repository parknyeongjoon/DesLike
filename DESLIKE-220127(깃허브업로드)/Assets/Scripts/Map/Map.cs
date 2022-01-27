using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SerializableMapNode
{
    public MapNode[] mapNodesY;
}

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class Map : ScriptableObject
{
    public SerializableMapNode[] mapNodesX;
    public MapNode curMapNode;
    public List<SoldierData> soldierRewardList;
    public List<GameObject> relicRewardList;
    public bool isMap = false;
    public int playerX= -1, playerY;
    public int level;

    public void MapClear()
    {
        playerX = -1;
        for (int i = 0; i < 35; i++)
        {
            mapNodesX[i / 5].mapNodesY[i % 5].isNode = false;
            mapNodesX[i / 5].mapNodesY[i % 5].isPlayable = false;
            mapNodesX[i / 5].mapNodesY[i % 5].isVisited = false;
            for (int j = 0; j < 3; j++)
            {
                mapNodesX[i / 5].mapNodesY[i % 5].isPath[j] = false;
            }
        }
    }

    public void CheckPlayableNode()
    {
        for (int i = 0; i < 35; i++)
        {
            mapNodesX[i/5].mapNodesY[i%5].isPlayable = false;
        }
        if (playerX == -1)
        {
            for (int i = 0; i < 5; i++)
            {
                mapNodesX[0].mapNodesY[i].isPlayable = true;
            }
        }
        else if (playerX < 6)
        {
            for (int i = 0; i < 3; i++)
            {
                if (mapNodesX[playerX].mapNodesY[playerY].isPath[i])
                {
                    mapNodesX[playerX + 1].mapNodesY[playerY + i - 1].isPlayable = true;
                }
            }
        }
    }

    public void EndMapNode()//각 노드에서 수행?
    {
        playerX = curMapNode.x; playerY = curMapNode.y;
        CheckPlayableNode();
        curMapNode.isVisited = true;
        SceneManager.LoadScene("Map");
    }
}
