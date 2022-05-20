using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class Map : ScriptableObject
{
    public MapNode curMapNode;
    const int THREE = 3;
    public Kingdom kingdom;
    public List<MapNode> selectNode = new List<MapNode>(new MapNode[THREE]);    // 선택지 3개 저장

    public List<SoldierData> physicNorSol;
    public List<SoldierData> physicEpicSol;

    public List<SoldierData> spellNorSol;
    public List<SoldierData> spellEpicSol;

    public Dictionary<string, Relic> physicNorRel;
    public Dictionary<string, Relic> physicEpicRel;
    public Dictionary<string, Relic> physicLegendRel;

    public Dictionary<string, Relic> spellNorRel;
    public Dictionary<string, Relic> spellEpicRel;
    public Dictionary<string, Relic> spellLegendRel;

    public Dictionary<string, Relic> commonNorRel;
    public Dictionary<string, Relic> commonEpicRel;
    public Dictionary<string, Relic> commonLegendRel;
    
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
