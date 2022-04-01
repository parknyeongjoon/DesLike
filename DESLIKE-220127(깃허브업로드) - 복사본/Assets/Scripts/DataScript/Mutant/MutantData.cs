using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutantData", menuName = "ScriptableObject/Mutant")]
public class MutantData : ScriptableObject
{
    public Sprite mutantImg;
    public string code;
    public string mutantName;
    public string toolTip;
    public Rarity rarity;
}
