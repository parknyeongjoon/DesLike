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

    public List<Relic> physicNorRel;
    public List<Relic> physicEpicRel;
    public List<Relic> physicLegendRel;

    public List<Relic> spellNorRel;
    public List<Relic> spellEpicRel;
    public List<Relic> spellLegendRel;
    public List<Relic> commonNorRel;
    public List<Relic> commonEpicRel;
    public List<Relic> commonLegendRel;
    
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
