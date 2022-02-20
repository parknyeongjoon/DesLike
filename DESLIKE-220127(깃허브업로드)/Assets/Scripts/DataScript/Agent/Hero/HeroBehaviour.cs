using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroBehaviour : SoldierBasic
{
    Coroutine moveCoroutine;

    new void Start()
    {
        base.Start();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(moveCoroutine != null) { StopCoroutine(moveCoroutine); }
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            heroInfo.moveDir = (new Vector3(mousePos.x,mousePos.y,0) - transform.position).normalized;
            moveCoroutine = StartCoroutine(Move(mousePos));
        }
        else if (Input.GetMouseButtonDown(2))
        {
            //기본 공격 추가;
        }
    }

    IEnumerator Move(Vector3 destination)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * (((HeroData)heroInfo.castleData).speed + heroInfo.buff_Stat.speed));
            yield return new WaitForFixedUpdate();
        }
    }
}
