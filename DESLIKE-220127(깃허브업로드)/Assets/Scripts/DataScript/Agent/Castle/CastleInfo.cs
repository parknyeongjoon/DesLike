using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleInfo : MonoBehaviour
{
    public CastleData castleData;
    public float cur_Hp;
    [SerializeField]
    protected PortDatas portDatas;

    public delegate void DeadHandler();
    public DeadHandler beforeDeadHandler;//죽기 전에 발동한 효과들을 넣는 델리게이트
    public DeadHandler afterDeadHandler;//진짜 죽었을 때 나올 효과들을 넣을 델리게이트

    void Start()
    {
        cur_Hp = castleData.hp;
    }

    public void Die()
    {
        if (beforeDeadHandler != null)
        {
            beforeDeadHandler.Invoke();
        }
        else
        {
            afterDeadHandler?.Invoke();
        }
    }

    public void OnDamaged(float damage)//이벤트로 분리하기
    {
        cur_Hp -= (damage - castleData.def);
        //힐 가중치 계산
        if (castleData.blood != null)
        {
            StartCoroutine(Bleeding());
        }
        if (cur_Hp <= 0)
        {
            Die();
        }
    }

    public void OnHealed(float heal)//이벤트로 분리하기
    {
        if(cur_Hp + heal >= castleData.hp)
        {
            cur_Hp = castleData.hp;
        }
        else
        {
            cur_Hp += heal;
        }
        //힐 가중치 계산
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
