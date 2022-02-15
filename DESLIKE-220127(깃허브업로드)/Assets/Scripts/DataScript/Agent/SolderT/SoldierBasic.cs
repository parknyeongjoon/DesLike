 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierBasic : MonoBehaviour
{
    public HeroInfo heroInfo;

    public Func<CastleInfo, IEnumerator> atkHandler;
    public Action atkDetect;
    public Func<bool> canAtk;

    protected void Start()
    {
        heroInfo.moveDir = new Vector3(0, 0, 0);
        StartCoroutine(Idle_Behaviour());
    }

    protected virtual IEnumerator Idle_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Detect_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Battle_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Siege_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual IEnumerator Stun_Behaviour()
    {
        yield return new WaitForFixedUpdate();
    }

    protected virtual void Dead_Behaviour()
    {

    }

    protected void Move()
    {
        transform.Translate(Time.deltaTime * ((HeroData)heroInfo.castleData).speed * heroInfo.moveDir);
    }
}
