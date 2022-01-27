using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    Map map;
    [SerializeField]
    Canvas EventCanvas;
    EventNode eventNode;

    void OnEnable()
    {
        map = SaveManager.Instance.gameData.map;
        eventNode = (EventNode)map.curMapNode;
        EventCanvas.transform.Find(eventNode.eventName).gameObject.SetActive(true);
    }

    public void EndEvent()
    {
        map.EndMapNode();
    }
}
