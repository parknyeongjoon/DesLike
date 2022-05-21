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

    public Dictionary<string, Relic> physicNorRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> physicEpicRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> physicLegendRel = new Dictionary<string, Relic>();

    public Dictionary<string, Relic> spellNorRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> spellEpicRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> spellLegendRel = new Dictionary<string, Relic>();

    public Dictionary<string, Relic> commonNorRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> commonEpicRel = new Dictionary<string, Relic>();
    public Dictionary<string, Relic> commonLegendRel = new Dictionary<string, Relic>();
    
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기
    
    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
