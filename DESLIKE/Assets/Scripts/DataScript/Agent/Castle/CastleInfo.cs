using System.Collections;
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
    public Action hitEvent;//캐릭터 피격 시 발동하는 이벤트
    public UnityEvent healthChangeEvent;//피가 변할 때 일어나는 이벤트

    public float cur_Hp;

    void Start()
    {
        cur_Hp = castleData.hp;
        hitEvent += healthChangeEvent.Invoke;
    }

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

    public virtual void OnDamaged(float damage)
    {
        hitEvent?.Invoke();
        cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        if (castleData.blood != null)
        {
            StartCoroutine(Bleeding());
        }
        if (cur_Hp <= 0)
        {
            Debug.Log("사망");
            Die();
        }
    }

    public virtual void OnHealed(float heal)
    {
        if(cur_Hp + heal >= castleData.hp)
        {
            cur_Hp = castleData.hp;
        }
        else
        {
            cur_Hp += heal;
        }
    }
    //피격 유혈 효과
    IEnumerator Bleeding()
    {
        GameObject createBlood;
        createBlood = Instantiate(castleData.blood, transform);
        yield return new WaitForSeconds(0.8f);
        Destroy(createBlood);
    }
}
