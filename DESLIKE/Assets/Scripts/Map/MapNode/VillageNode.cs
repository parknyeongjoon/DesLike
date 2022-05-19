using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "VillageNode", menuName = "ScriptableObject/MapNodeT/VillageNode")]
public class VillageNode : MapNode
{
    void OnEnable()
    {
        SetAbleReward();
    }

    public void Play_VillageNode()
    {
        map.curMapNode = this;
        SceneManager.LoadScene("Village");
    }

    public void End_VillageNode()
    {
        SaveManager.Instance.gameData.villageData = new VillageData();  // ������ �ʱ�ȭ
        SceneManager.LoadScene("Map");
    }
}