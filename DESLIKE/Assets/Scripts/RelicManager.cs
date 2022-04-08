using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicManager : MonoBehaviour
{
    static RelicManager instance;

    public List<Relic> relicList = new List<Relic>();

    public Canvas relicCanvas;

    public delegate void SoldierConditionHandler(SoldierData soldierData);
    public SoldierConditionHandler soldierConditionHandler;

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
        //ΩÃ±€≈Ê ∆–≈œ
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckRelicCondition(SoldierData soldierData)
    {
        soldierConditionHandler(soldierData);
    }
}