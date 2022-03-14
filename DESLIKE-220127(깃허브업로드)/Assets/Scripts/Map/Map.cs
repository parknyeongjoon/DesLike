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

    public int curStage = 0;
    public int curDay;  // 추가(by 시후), 맵 날짜 체크용
    public int[] nextEvent = new int[3];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부
    
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
    /*
    public void CheckPlayableNode() // 맵 양식 바뀌고 필요없어짐
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
    */

    public void EndMapNode()    //각 노드에서 수행?
    {
        playerX = curMapNode.x; playerY = curMapNode.y;
        // CheckPlayableNode();
        // curMapNode.isVisited = true;
        SceneManager.LoadScene("Map");
    }
}
