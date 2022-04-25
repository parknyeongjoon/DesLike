﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CastleInfo : MonoBehaviour
{
    public CastleData castleData;

    public PortDatas allyPortDatas;
    public PortDatas enemyPortDatas;

    public Action beforeDeadEvent;//죽기 전에 발동하는 이벤트(부활)
    public UnityEvent afterDeadEvent;//죽고 난 뒤 일어나는 이벤트(시체가 터진다거나)
    public Action<HeroInfo> hitEvent;//캐릭터 피격 시 발동하는 이벤트
    public UnityEvent<HeroInfo> healthChangeEvent;//피가 변할 때 일어나는 이벤트

    public float cur_Hp;

    public void Die()//DeadBehaviour로 넘겨버리기
    {
        if (beforeDeadEvent != null)
        {
            Debug.Log("사망 전 이벤트");
            beforeDeadEvent?.Invoke();
        }
        else
        {
            Debug.Log("사망 후 이벤트");
            afterDeadEvent?.Invoke();
        }
    }
}
