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
        if (SaveManager.Instance.dataSheet.relicDataSheet[relicKey])//데이터 시트에 있는 유물인지 검사
        {
            GameObject relicObject = Instantiate(SaveManager.Instance.dataSheet.relicObjectSheet[relicKey], relicCanvas.transform.GetChild(0).transform);
            relicList.Add(relicKey, relicObject.GetComponent<Relic>());
            relicList[relicKey].DoEffect();
        }
        else
        {
            Debug.Log("유물 키 없음");
        }
    }

    public void LoadRelic(string relicKey)
    {
        if (SaveManager.Instance.dataSheet.relicDataSheet[relicKey])//데이터 시트에 있는 유물인지 검사
        {
            GameObject relicObject = Instantiate(SaveManager.Instance.dataSheet.relicObjectSheet[relicKey], relicCanvas.transform.GetChild(0).transform);
            relicList.Add(relicKey, relicObject.GetComponent<Relic>());
            if (relicList[relicKey].relicData.continueReuse)//로드하면 다시 사용해야하는 유물이라면 재사용
            {
                relicList[relicKey].DoEffect();
            }
        }
        else
        {
            Debug.Log("유물 키 없음");
        }
    }

    public void DestroyRelic(string relicKey)
    {
        if (relicList.ContainsKey(relicKey))
        {
            Destroy(relicList[relicKey].gameObject);
            relicList.Remove(relicKey);
        }
    }
}