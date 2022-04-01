﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PortData", menuName = "ScriptableObject/PortData")]
public class PortData : ScriptableObject
{
    public string soldierCode;
    public bool unlock;
    public Image portImg;
}