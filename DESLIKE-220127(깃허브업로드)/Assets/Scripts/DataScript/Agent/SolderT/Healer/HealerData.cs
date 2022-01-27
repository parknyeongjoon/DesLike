using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData",menuName = "ScriptableObject/Agent/HealerData")]
public class HealerData : SoldierData
{
    [SerializeField]
    GameObject healEffect;
    [SerializeField]
    float healMp;

    public float HealMp { get => healMp; set => healMp = value; }
    public GameObject HealEffect { get => healEffect; set => healEffect = value; }
}
