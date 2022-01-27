using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoodsCollection", menuName = "ScriptableObject/GoodsCollection")]
public class GoodsCollection : ScriptableObject
{
    public float food;
    public float foodIncome;
    public int gold;
    public int magicalStone;
    public int scrap;
}
