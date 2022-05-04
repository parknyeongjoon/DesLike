using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn",menuName ="ScriptableObject/ExtraSkill/Spawn")]
public class Spawn : ActiveSkillData
{
    public GameObject spawnPrefab;
    public int spawnAmount;

    public override void Effect(HeroInfo heroInfo, HeroInfo targetInfo)
    {
        heroInfo.StartCoroutine(SpawnPrefab(heroInfo));
    }

    IEnumerator SpawnPrefab(HeroInfo heroInfo)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(spawnPrefab, heroInfo.transform.position + new Vector3(2, 0, spawnAmount - 2 * i), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
