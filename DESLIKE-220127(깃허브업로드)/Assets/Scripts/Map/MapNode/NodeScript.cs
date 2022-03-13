using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    [SerializeField] MapNode mapNode;

    protected Image nodeImg;

    public MapNode MapNode { get => mapNode; set => mapNode = value; }

    void OnEnable()
    {
        StartCoroutine(SetMapNodeImg());
    }

    IEnumerator SetMapNodeImg()
    {
        while (!mapNode.map.isMap)
        {
            yield return null;
        }
        if (!mapNode.isNode)
        {
            gameObject.SetActive(false);
        }
       
        nodeImg = transform.GetComponent<Image>();
        if (gameObject.activeSelf)
        {
            if (mapNode.isVisited)
            {
                nodeImg.color = new Color(0.25f, 0.25f, 0.25f);
            }
            if (mapNode.isPlayable)
            {
                StartCoroutine(Playable_Effect());
            }
        }
    }
    //플레이 가능한 노드는 깜빡거리게하는 함수
    IEnumerator Playable_Effect()
    {
        float time = 0;

        while (true)
        {
            if(time < 0.5f)
            {
                nodeImg.color = new Color(1, 1, 1, 1 - time);
            }
            else
            {
                nodeImg.color = new Color(1, 1, 1, time);
                if(time > 1.0f)
                {
                    time = 0;
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void SetReward()
    {
        int soldierRewardIndex = Random.Range(0, mapNode.rewardSoldierList.Count);
        mapNode.reward.soldierReward.soldier = mapNode.rewardSoldierList[soldierRewardIndex];
        int relicRewardIndex = Random.Range(0, mapNode.rewardRelicList.Count);
        mapNode.reward.relic = mapNode.rewardRelicList[relicRewardIndex];
    }
}
