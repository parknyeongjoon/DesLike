using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : Mutant
{
    void OnEnable()
    {
        StartCoroutine(RecoverEffect());
    }

    IEnumerator RecoverEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            heroInfo.OnHealed(heroData.hp * 0.05f);
        }
    }
}
