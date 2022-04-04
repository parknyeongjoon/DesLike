using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObject/Map")]
public class Map : ScriptableObject
{
    public MapNode curMapNode;//지우기
    public List<SoldierData> soldierRewardList;
    public List<GameObject> relicRewardList;
    public bool isMap = false;//지우기
    public int playerX= -1, playerY;//지우기
    public int level;//지우기

    public void EndMapNode()
    {
        SceneManager.LoadScene("Map");
    }
}
