using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    Map map;
    [SerializeField]
    NodeScript[] nodeScripts;

    void Awake()
    {
        map = SaveManager.Instance.gameData.map;

        CreateMap();
        map.CheckPlayableNode();
    }

    public void CreateMap()
    {
        if (!map.isMap)
        {
            for (int i = 0; i < 35; i++)
            {
                map.mapNodesX[i / 5].mapNodesY[i % 5] = nodeScripts[i].MapNode;
                if (nodeScripts[i] as BattleNodeScript)
                {
                    nodeScripts[i].SetReward();
                }
            }
            map.MapClear();//필요한가?
            for (int line = 0; line < 5; line++)
            {
                CreateLine(line);
            }
            int temp = Random.Range(0, 5);
            CreateLine(temp);
            map.isMap = true;
            SaveManager.Instance.gameData.canContinue = true;
            SaveManager.Instance.SaveGameData();
        }
    }

    void CreateLine(int line)
    {
        int lineMove;
        map.mapNodesX[6].mapNodesY[line].isNode = true;
        for (int i = 6; i > 0; i--)
        {
            lineMove = Random.Range(-1, 2);
            line += lineMove;
            if (line < 0)
            {
                lineMove = 0;
                line = 0;
            }
            else if (line > 4)
            {
                lineMove = 0;
                line = 4;
            }
            map.mapNodesX[i - 1].mapNodesY[line].isPath[-lineMove + 1] = true;
            map.mapNodesX[i - 1].mapNodesY[line].eventableDot[-lineMove + 1] = Random.Range(4, 10);
            map.mapNodesX[i - 1].mapNodesY[line].isNode = true;
        }
    }
}