using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBossScript : MonoBehaviour
{
    [SerializeField]
    SoldierInfo soldierInfo;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => BattleUIManager.Instance.battleStart == true);
        StartCoroutine(P2());
    }

    IEnumerator P2()
    {
        while (true)
        {
            if (soldierInfo.cur_Hp < soldierInfo.castleData.hp * 0.5f)
            {
                AkSoundEngine.PostEvent("Music_S1_Boss_P2", gameObject);
                yield break;
            }
            yield return null;
        }
    }
}
