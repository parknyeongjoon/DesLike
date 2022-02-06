 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBasic : MonoBehaviour
{
    public HeroInfo heroInfo;

    protected void Start()
    {
        heroInfo = GetComponent<HeroInfo>();
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

    protected void Move()
    {
        transform.Translate(Time.deltaTime * ((HeroData)heroInfo.castleData).speed * heroInfo.moveDir);
    }
}
