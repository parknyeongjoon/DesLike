using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;

    public Dictionary<string, Relic> relicList;

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

    // relic instantiate «ÿæﬂ«‘

    void Awake()
    {
        //ΩÃ±€≈Ê ∆–≈œ
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddRelicInCanvas()
    {
        // relicList.ContainsKey()
        // Instantiate(relicList[relicList.Count - 1], relicCanvas.transform.GetChild(0).transform);
    }
}