using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    Map map;
    [SerializeField]
    Canvas EventCanvas;
    [SerializeField]
    GameObject Thief, Training, Merchant, Rullet, CampFire, Infection;
    EventNode eventNode;
    int EvntList;

    void OnEnable()
    {
        map = SaveManager.Instance.gameData.map;
        eventNode = (EventNode)map.curMapNode;
        EventCanvas.transform.Find(eventNode.eventName).gameObject.SetActive(true);
        EvntList = Random.Range(0, 6);
        EventActive();
    }

    void EventActive()
    {
        switch (EvntList)
        {
            case 0:
                Thief.SetActive(true);
                break;
            case 1:
                Training.SetActive(true);
                break;
            case 2:
                Merchant.SetActive(true);
                break;
            case 3:
                Rullet.SetActive(true);
                break;
               case 4:
                CampFire.SetActive(true);
                break;
            case 5:
                Infection.SetActive(true);
                break;
            default: break;
        }
    }

    void EventInactive()
    {
        Debug.Log("꺼지는 거 실행");
        switch (EvntList)
        {
            case 0:
                Thief.SetActive(false);
                break;
            case 1:
                Training.SetActive(false);
                break;
            case 2:
                Merchant.SetActive(false);
                break;
            case 3:
                Rullet.SetActive(false);
                break;
            case 4:
                CampFire.SetActive(false);
                break;
            case 5:
                Infection.SetActive(false);
                break;
            default: break;
        }
        Debug.Log("꺼지는 거 실행 완료");

    }

    public void EndEvent()
    {
        EventInactive();
        map.EndMapNode();
    }
}
