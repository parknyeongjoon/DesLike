using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicBtnScript : MonoBehaviour
{
    [SerializeField]
    Image soldier_Img;

    public SoldierData soldierData;

    void Start()
    {
        soldier_Img.sprite = soldierData.sprite;
        //발견하지 않았으면 검은 색으로 처리 클릭도 비활성화
        //soldier_Img.color = new Color(0, 0, 0, 1);
    }
}
