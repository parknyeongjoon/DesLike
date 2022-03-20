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
    public int curTrack;
    public int checkDay; // 맵 날짜 체크용 
    public int curDay;  // 현재 날짜
    public int[] selEvent = new int[3];
    public int[] nextEvent = new int[3];   // 현재 이벤트(전투) 저장용. 0 : 1트랙, 1 : 2트랙, 2 : 3트랙
    public bool midBossCheck1 = false, midBossCheck2 = false, villageCheck = false, organCheck = false; // 중간 보스, 마을, 정비 여부



    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
