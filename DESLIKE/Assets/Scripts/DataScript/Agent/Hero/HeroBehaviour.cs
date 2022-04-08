using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroBehaviour : SoldierBasic
{
    new void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (heroInfo.action != Soldier_Action.End_Delay)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (moveCoroutine != null) { StopCoroutine(moveCoroutine); }
                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                heroInfo.moveDir = (new Vector3(mousePos.x, mousePos.y, 0) - transform.position).normalized;
                moveCoroutine = StartCoroutine(Move(mousePos));
            }
            else if (Input.GetMouseButtonDown(2))
            {
                //기본 공격 추가;
            }
        }
    }
}
