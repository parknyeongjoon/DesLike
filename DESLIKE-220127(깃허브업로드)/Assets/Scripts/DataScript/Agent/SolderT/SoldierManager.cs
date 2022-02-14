using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    //º´»ç Å‰µæ ÇÔ¼ö
    public static void GetSoldier(string code, GameObject mutant, int remain)
    {
        SoldierData getSoldier = Instantiate(SaveManager.Instance.dataSheet.soldierDataSheet[code]);
        getSoldier.mutant = mutant;
        getSoldier.remain = remain;
        RelicManager.Instance.CheckRelicCondition(getSoldier);
        SaveManager.Instance.activeSoldierList.Add(code, getSoldier);
    }
}
