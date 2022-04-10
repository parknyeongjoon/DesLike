using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class Map : ScriptableObject
{
    public MapNode curMapNode;
    public List<MapNode> selectNode;    // 선택지 3개 저장

    public List<SoldierData> physicNorSol;
    public List<SoldierData> physicEpicSol;

    public List<SoldierData> spellNorSol;
    public List<SoldierData> spellEpicSol;

    public List<SoldierData> commonNorSol;
    public List<SoldierData> commonEpicSol;

    public List<GameObject> physicNorRel;
    public List<GameObject> physicEpicRel;
    public List<GameObject> physicLegendRel;

    public List<GameObject> spellNorRel;
    public List<GameObject> spellEpicRel;
    public List<GameObject> spellLegendRel;
    public List<GameObject> commonNorRel;
    public List<GameObject> commonEpicRel;
    public List<GameObject> commonLegendRel;
    
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
