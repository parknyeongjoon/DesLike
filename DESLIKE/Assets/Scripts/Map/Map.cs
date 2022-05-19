using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class Map : ScriptableObject
{
    public MapNode curMapNode;
    const int THREE = 3;
    public List<MapNode> selectNode = new List<MapNode>(new MapNode[THREE]);    // 선택지 3개 저장

    public List<SoldierData> physicNorSol;
    public List<SoldierData> physicEpicSol;

    public List<SoldierData> spellNorSol;
    public List<SoldierData> spellEpicSol;

    public List<SoldierData> commonNorSol;
    public List<SoldierData> commonEpicSol;

    public Dictionary<string, RelicData> physicNorRel;
    public Dictionary<string, RelicData> physicEpicRel;
    public Dictionary<string, RelicData> physicLegendRel;

    public Dictionary<string, RelicData> spellNorRel;
    public Dictionary<string, RelicData> spellEpicRel;
    public Dictionary<string, RelicData> spellLegendRel;
    public Dictionary<string, RelicData> commonNorRel;
    public Dictionary<string, RelicData> commonEpicRel;
    public Dictionary<string, RelicData> commonLegendRel;
    
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
