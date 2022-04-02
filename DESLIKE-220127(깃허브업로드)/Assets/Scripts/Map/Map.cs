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

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
