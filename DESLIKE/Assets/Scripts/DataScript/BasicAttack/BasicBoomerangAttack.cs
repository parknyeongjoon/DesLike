using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicBoomerangAttack",menuName ="ScriptableObject/BasicAttack/BasicBoomerangAttack")]
public class BasicBoomerangAttack : BasicSingleRangeAttackData
{
    public int bounceCount;
    public float bounceArea;

    protected override IEnumerator ShootArrow(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        Debug.Log("�θ޶�");
        GameObject createArrow;
        Vector3 startPos = heroInfo.transform.position;
        Transform desTrans = targetInfo.transform;
        HeroInfo arrowTargetInfo = targetInfo;
        createArrow = Instantiate(arrow, heroInfo.transform.position, Quaternion.identity);//������Ʈ Ǯ��
        for(int i = 0; i < bounceCount; i++)
        {
            float shotTime = 0.0f;
            while (shotTime < arrowSpeed)
            {
                shotTime += Time.deltaTime;
                createArrow.transform.position = Vector2.Lerp(startPos, desTrans.position, shotTime / arrowSpeed);
                yield return new WaitForFixedUpdate();
            }
            if (arrowTargetInfo)
            {
                ChargeMP(heroInfo);
                arrowTargetInfo.OnDamaged(heroInfo, atk_Dmg);
                extraSkillData?.Effect(heroInfo, arrowTargetInfo);
            }
            GameObject targetObject = BounceDetect(heroInfo, arrowTargetInfo, createArrow.transform.position);
            if (targetObject == null) { break; }//ƨ�� ����� ���ٸ� ����
            else //ƨ�� ����� �ִٸ� ����
            {
                Debug.Log(targetObject.name);
                startPos = createArrow.transform.position;
                desTrans = targetObject.transform;
                arrowTargetInfo = targetObject.GetComponent<HeroInfo>();
            }
        }
        Destroy(createArrow);
    }

    GameObject BounceDetect(HeroInfo heroInfo, HeroInfo targetInfo, Vector3 boomerangPos)//�θ޶��� ƨ�� ���� Ž���ϴ� �Լ�
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(boomerangPos, bounceArea, ((int)heroInfo.team ^ 7) * (int)atkArea);
        if (targets != null && targets.Length > 1)
        {
            GameObject tempObject;
            int temp;
            temp = Random.Range(0, targets.Length);
            tempObject = targets[temp].gameObject;
            while(tempObject == targetInfo.gameObject)//���� ��� ���� ���ϰ� �ϴ� �Լ�, ���� �Ͼ �� ���� - count�� Ž�� Ƚ�� ���� �α�
            {
                temp = Random.Range(0, targets.Length);
                tempObject = targets[temp].gameObject;
            }
            return tempObject;
        }
        return null;
    }
}
