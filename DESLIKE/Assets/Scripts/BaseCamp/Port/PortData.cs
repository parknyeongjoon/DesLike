using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PortData", menuName = "ScriptableObject/PortData")]
public class PortData : ScriptableObject
{
    public string soldierCode = "";
    public string mutantCode = "";
    public bool unlock = false;
    public Image portImg;
}