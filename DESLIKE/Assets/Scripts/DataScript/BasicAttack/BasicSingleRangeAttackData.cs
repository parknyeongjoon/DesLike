using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicSingleRangeAttack", menuName = "ScriptableObject/BasicAttack/BasicSingleRangeAttack")]
public class BasicSingleRangeAttackData : BasicAttackData
{
    public GameObject arrow;
    public float arrowSpeed;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        targetInfo.StartCoroutine(ShootArrow(heroInfo, targetInfo));
    }

    IEnumerator ShootArrow(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        float shotTime = 0.0f;
        GameObject createArrow;
        Transform desTrans = targetInfo.transform;
        HeroInfo castleInfo = targetInfo;
        createArrow = Instantiate(arrow);
        while (shotTime < arrowSpeed)
        {
            shotTime += Time.deltaTime;
            createArrow.transform.position = Vector2.Lerp(createArrow.transform.position, desTrans.position, shotTime / arrowSpeed);
            yield return null;
        }
        if (castleInfo) 
        {
            castleInfo.OnDamaged(atk_Dmg); extraSkillData?.Effect(heroInfo, targetInfo);
        }
        Destroy(createArrow);
    }
}
