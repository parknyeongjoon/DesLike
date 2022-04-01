using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoodsCollection", menuName = "ScriptableObject/GoodsCollection")]
public class GoodsCollection : ScriptableObject
{
    public int gold;
    public int magicalStone;
    public int scrap;
}