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
        Thief.SetActive(false);
        Training.SetActive(false);
        Merchant.SetActive(false);
        Rullet.SetActive(false);
        CampFire.SetActive(false);
        Infection.SetActive(false);
        
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
}
