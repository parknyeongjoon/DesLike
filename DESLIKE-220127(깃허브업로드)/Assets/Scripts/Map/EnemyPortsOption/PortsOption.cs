using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortsOption", menuName = "ScriptableObject/PortsOption")]
public class PortsOption : ScriptableObject
{
    [SerializeField]
    List<Option> soldierOption;

    public List<Option> SoldierOption { get => soldierOption;}
}
