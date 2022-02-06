using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleRangeAttack", menuName = "ScriptableObject/BasicAttack/BasicSingleRangeAttack")]
public class BasicSingleRangeAttackData : BasicSingleAttackData
{
    public GameObject arrow;
    public int ammo;
    public float arrowSpeed;

    public IEnumerator Effect(MonoBehaviour caller, CastleInfo targetInfo, HeroInfo heroInfo)
    {
        yield return new WaitForSeconds(start_Delay);
        if (targetInfo)
        {
            //soldierBehaviour.animator.SetTrigger("isAtk");
            caller.StartCoroutine(ShootArrow(heroInfo));
            heroInfo.action = Soldier_Action.End_Delay;
            yield return new WaitForSeconds(end_Delay);
        }
        heroInfo.action = Soldier_Action.Idle;
    }

    IEnumerator ShootArrow(HeroInfo heroInfo)//화살 쓰게 추가하기
    {
        float shotTime = 0.0f;
        GameObject createArrow;
        createArrow = Instantiate(arrow, heroInfo.transform);
        while (shotTime < arrowSpeed)
        {
            shotTime += Time.deltaTime;
            createArrow.transform.position = Vector2.Lerp(createArrow.transform.position, heroInfo.target.transform.position, shotTime / arrowSpeed);
            yield return null;
        }
        if (heroInfo.targetInfo) { heroInfo.targetInfo.OnDamaged(atk_Dmg); }
        Destroy(createArrow);
    }
}
