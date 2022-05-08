using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGrenadeRangeAttackData",menuName = "ScriptableObject/BasicAttack/BasicGrenadeRangeAttackData")]
public class BasicGrenadeRangeAttackData : BasicGrenadeAttackData
{
    public GameObject arrow;
    public float arrowSpeed;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        BattleUIManager.Instance.StartCoroutine(ShootArrow(heroInfo, targetInfo));
    }

    IEnumerator ShootArrow(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        float shotTime = 0.0f;
        GameObject createArrow;
        Transform desTrans = targetInfo.transform;//직접 참조 중인지 확인해봐야함
        HeroInfo castleInfo = targetInfo;
        createArrow = Instantiate(arrow, heroInfo.transform.position, Quaternion.identity);//오브젝트 풀링
        while (shotTime < arrowSpeed)
        {
            shotTime += Time.deltaTime;
            createArrow.transform.position = Vector2.Lerp(heroInfo.transform.position, desTrans.position, shotTime / arrowSpeed);
            yield return new WaitForFixedUpdate();
        }
        if (castleInfo)
        {
            List<HeroInfo> targetInfos;
            targetInfos = Get_Targets(heroInfo, targetInfo);
            ChargeMP(heroInfo);
            for (int i = 0; i < targetInfos.Count; i++)
            {
                targetInfos[i].OnDamaged(heroInfo, atk_Dmg);
                extraSkillData?.Effect(heroInfo, targetInfo);
            }
        }
        Destroy(createArrow);
    }
}
