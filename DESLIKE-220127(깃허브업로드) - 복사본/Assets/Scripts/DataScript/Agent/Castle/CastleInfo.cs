using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleInfo : MonoBehaviour
{
    public CastleData castleData;
    public float cur_Hp;
    public PortDatas portDatas;

    public delegate void DeadHandler();
    public DeadHandler beforeDeadHandler;//죽기 전에 발동한 효과들을 넣는 델리게이트
    public DeadHandler afterDeadHandler;//진짜 죽었을 때 나올 효과들을 넣을 델리게이트
    public UnityEvent deadEvent;

    void Start()
    {
        cur_Hp = castleData.hp;
    }

    public void Die()//DeadBehaviour로 넘겨버리기
    {
        if (beforeDeadHandler != null)
        {
            beforeDeadHandler.Invoke();
        }
        else
        {
            afterDeadHandler?.Invoke();
            deadEvent?.Invoke();
        }
    }

    public virtual void OnDamaged(float damage)
    {
        cur_Hp -= (damage - castleData.def);//버프 스탯 넣기, 수식 설정하기
        if (castleData.blood != null)
        {
            StartCoroutine(Bleeding());
        }
        if (cur_Hp <= 0)
        {
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
