using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;

    public List<Relic> relicList = new List<Relic>();

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
}