using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{
    public MutantData mutantData;
    [SerializeField]
    protected HeroInfo heroInfo;
    [SerializeField]
    protected HeroData heroData;

    void Start()
    {
        heroInfo = transform.GetComponentInParent<HeroInfo>();
        heroData = (HeroData)heroInfo.castleData;
    }
}
