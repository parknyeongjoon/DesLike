using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleRangeAttack", menuName = "ScriptableObject/BasicAttack/BasicSingleRangeAttack")]
public class BasicSingleRangeAttackData : BasicAttackData
{
    public GameObject arrow;
    public int ammo;
    public float arrowSpeed;

    public void Effect(MonoBehaviour caller, CastleInfo targetInfo, Transform startTrans)
    {
        caller.StartCoroutine(ShootArrow(targetInfo, startTrans));
    }

    IEnumerator ShootArrow(CastleInfo targetInfo, Transform startTrans)//화살 쓰게 추가하기, heroInfo.targetInfo말고 저장해놓고 걔인지 체크하기
    {
        float shotTime = 0.0f;
        GameObject createArrow;
        Transform desTrans = targetInfo.transform;
        CastleInfo castleInfo = targetInfo;
        createArrow = Instantiate(arrow, startTrans);
        while (shotTime < arrowSpeed)
        {
            shotTime += Time.deltaTime;
            createArrow.transform.position = Vector2.Lerp(createArrow.transform.position, desTrans.position, shotTime / arrowSpeed);
            yield return null;
        }
        if (castleInfo) { castleInfo.OnDamaged(atk_Dmg); }
        Destroy(createArrow);
    }
}
