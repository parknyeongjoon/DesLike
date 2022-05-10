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
        BattleUIManager.Instance.StartCoroutine(ShootArrow(heroInfo, targetInfo));
    }

    protected virtual IEnumerator ShootArrow(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        float shotTime = 0.0f;
        GameObject createArrow;
        Transform desTrans = targetInfo.transform;//���� ���� ������ Ȯ���غ�����
        HeroInfo castleInfo = targetInfo;
        createArrow = Instantiate(arrow, heroInfo.transform.position, Quaternion.identity);//������Ʈ Ǯ��
        while (shotTime < arrowSpeed)
        {
            shotTime += Time.deltaTime;
            createArrow.transform.position = Vector2.Lerp(heroInfo.transform.position, desTrans.position, shotTime / arrowSpeed);
            yield return new WaitForFixedUpdate();
        }
        if (castleInfo) 
        {
            ChargeMP(heroInfo);
            castleInfo.OnDamaged(heroInfo, atk_Dmg);
            extraSkillData?.Effect(heroInfo, targetInfo);
        }
        Destroy(createArrow);
    }
}
