using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//scriptable로 건들여보기
public class DetailPanelScript : MonoBehaviour
{
    public Image soldier_Img;
    public TMP_Text soldier_name, atk_Type, def_Type, tribe, hp, mp, range, atk, def, rarity, cost;

    public void Set_DetailPanel(SoldierData soldierData)//궁수와 힐러 케이스 추가
    {
        soldier_Img.sprite = soldierData.sprite;
        soldier_name.text = soldierData.soldier_name;
        //detailPanelScript.atk_Type.text = soldierData.AtkType;
        //detailPanelScript.def_Type.text = soldierData.DefType;
        //detailPanelScript.tribe.text = soldierData.Tribe;
        hp.text = soldierData.hp.ToString();
        mp.text = soldierData.mp.ToString();
        //detailPanelScript.range.text = soldierData.Range.ToString();//atkData 받아오기
        //detailPanelScript.atk.text = soldierData.Atk.ToString();atkData 받아오기
        def.text = soldierData.def.ToString();
        rarity.text = soldierData.rarity.ToString();
    }
}