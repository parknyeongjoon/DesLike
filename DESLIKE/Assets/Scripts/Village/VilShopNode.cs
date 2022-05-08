using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VilShopNode", menuName = "ScriptableObject/VilNode")]
public class VilShopNode : ScriptableObject
{
    public Relic curRelic;
    public int curNumber;

    public void GetRelic()
    {
        RelicManager.instance.relicList.Add(curRelic);
        curRelic = new Relic();
    }
}
