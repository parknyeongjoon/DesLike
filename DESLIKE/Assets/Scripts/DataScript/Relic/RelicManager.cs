using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;

    public Dictionary<string, Relic> relicList = new Dictionary<string, Relic>();

    public Canvas relicCanvas;

    public Action<HeroData> soldierConditionCheck;

    public static RelicManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        //싱글톤 패턴
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetRelic(string relicKey)//relicKey에 해당하는 유물을 흭득하는 함수
    {
        if (SaveManager.Instance.dataSheet.relicDataSheet[relicKey])
        {
            GameObject relicObject = SaveManager.Instance.dataSheet.relicObjectSheet[relicKey];
            relicList.Add(relicKey, relicObject.GetComponent<Relic>());
            Instantiate(relicObject, relicCanvas.transform.GetChild(0).transform);
            relicList[relicKey].DoEffect();
        }
        else
        {
            Debug.Log("유물 키 없음");
        }
    }
}